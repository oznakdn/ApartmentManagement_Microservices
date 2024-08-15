using Apartment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Core.MessageQueue.Models;
using Shared.Core.MessageQueue.Queues;
using System.Text;
using System.Text.Json;

namespace Apartment.Background.ConsumerServices;

public class DeleteManagerFromSiteConsumer
{
    private readonly IServiceProvider _serviceProvider;
    public DeleteManagerFromSiteConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ConsumeAsync(IModel channel)
    {

        channel.QueueDeclare(queue: SiteQueue.DELETE_MANAGER, durable: true, exclusive: false, autoDelete: false);


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (s, e) =>
        {

            string body = Encoding.UTF8.GetString(e.Body.ToArray());

            var dto = JsonSerializer.Deserialize<DeleteManagerFromSiteModel>(body);

            if (dto is not null)
            {
                using var scope = _serviceProvider.CreateScope();

                var queryDbContext = scope.ServiceProvider.GetRequiredService<QueryDbContext>();
                var commandDbContext = scope.ServiceProvider.GetRequiredService<CommandDbContext>();

                var siteQuery = await commandDbContext.Sites.SingleOrDefaultAsync(x => x.Id == dto.SiteId);

                if (siteQuery is not null)
                {
                    siteQuery.RemoveManager();
                    queryDbContext.Sites.Update(siteQuery);
                    await queryDbContext.SaveChangesAsync();

                    var siteCommand = await commandDbContext.Sites.SingleOrDefaultAsync(x => x.Id == dto.SiteId);
                    siteCommand.RemoveManager();
                    commandDbContext.Sites.Update(siteCommand);
                    await commandDbContext.SaveChangesAsync();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Manager removed from the site.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Site not found!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Site not removed!");
                Console.ResetColor();
            }

            channel.BasicAck(e.DeliveryTag, false);


        };


        channel.BasicConsume(queue: SiteQueue.DELETE_MANAGER,
            autoAck: false,
            consumer: consumer);

        await Task.CompletedTask;

    }
}
