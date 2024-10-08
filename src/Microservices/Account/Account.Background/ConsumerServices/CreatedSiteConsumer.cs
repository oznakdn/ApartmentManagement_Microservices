﻿using Account.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Constants;
using Shared.Core.Models;
using System.Text;
using System.Text.Json;

namespace Account.Background.ConsumerServices;

public class CreatedSiteConsumer
{
    private readonly IServiceProvider _serviceProvider;
    public CreatedSiteConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ConsumeAsync(IModel channel)
    {

        channel.QueueDeclare(queue: QueueConstant.SITE_CREATED, durable: true, exclusive: false, autoDelete: false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (s, e) =>
        {

            string body = Encoding.UTF8.GetString(e.Body.ToArray());

            var dto = JsonSerializer.Deserialize<CreateSiteModel>(body);

            if (dto is not null)
            {
                using var scope = _serviceProvider.CreateScope();

                var queryDbContext = scope.ServiceProvider.GetRequiredService<QueryDbContext>();
                var commandDbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                var userQuery = await commandDbContext.Users.SingleOrDefaultAsync(x => x.Id == dto.ManagerId);

                if (userQuery is not null)
                {
                    userQuery.AssignSite(dto.SiteId);
                    queryDbContext.Users.Update(userQuery);
                    await queryDbContext.SaveChangesAsync();

                    var userCommand = await commandDbContext.Users.SingleOrDefaultAsync(x => x.Id == dto.ManagerId);
                    userCommand.AssignSite(dto.SiteId);
                    commandDbContext.Users.Update(userCommand);
                    await commandDbContext.SaveChangesAsync();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Site Assigned To Manager!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Manager Not Found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Site not assigned!");
                Console.ResetColor();
            }

            channel.BasicAck(e.DeliveryTag, false);


        };


        channel.BasicConsume(queue: QueueConstant.SITE_CREATED,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;

    }

}
