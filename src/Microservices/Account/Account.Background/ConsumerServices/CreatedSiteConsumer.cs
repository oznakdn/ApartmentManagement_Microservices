using Account.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
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

        channel.QueueDeclare(queue: SiteQueue.SITE_CREATED, durable: true, exclusive: false, autoDelete: false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (s, e) =>
        {

            string body = Encoding.UTF8.GetString(e.Body.ToArray());

            var dto = JsonSerializer.Deserialize<CreateSiteModel>(body);

            if (dto is not null)
            {
                using var scope = _serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Id == dto.ManagerId);

                if (user is not null)
                {
                    user.AssignSite(dto.SiteId);
                    dbContext.Users.Update(user);
                    await dbContext.SaveChangesAsync();
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


        channel.BasicConsume(queue: SiteQueue.SITE_CREATED,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;

    }

}
