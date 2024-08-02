﻿using Account.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
using System.Text;
using System.Text.Json;

namespace Account.Background.ConsumerServices;

public class AssignedResidentToUnitConsumer
{
    private readonly IServiceProvider _serviceProvider;
    public AssignedResidentToUnitConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ConsumeAsync(IModel channel)
    {

        channel.QueueDeclare(queue: SiteQueue.ASSIGN_RESIDENT, durable: true, exclusive: false, autoDelete: false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (s, e) =>
        {

            string body = Encoding.UTF8.GetString(e.Body.ToArray());

            var dto = JsonSerializer.Deserialize<AssignResidentToUnitModel>(body);

            if (dto is not null)
            {
                using var scope = _serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == dto.ResidentId);

                if (user is not null)
                {
                    user.AssignUnit(dto.UnitId);
                    dbContext.Users.Update(user);
                    await dbContext.SaveChangesAsync();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Unit assigned to resident!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Resident not found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unit not assigned!");
                Console.ResetColor();
            }

            channel.BasicAck(e.DeliveryTag, false);


        };


        channel.BasicConsume(queue: SiteQueue.ASSIGN_RESIDENT,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;

    }
}
