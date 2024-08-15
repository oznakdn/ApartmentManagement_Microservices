using Apartment.Background.ConsumerServices;
using RabbitMQ.Client;

namespace Apartment.Background;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _factory;
    private readonly DeleteManagerFromSiteConsumer _deleteManagerFromSite;

    private readonly string Url;
    private IConnection _connection;
    private IModel _channel;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, DeleteManagerFromSiteConsumer deleteManagerFromSite)
    {
        _logger = logger;
        _configuration = configuration;


        Url = _configuration["RabbitMqOption:ConnectionString"]!;

        _factory = new ConnectionFactory
        {
            Uri = new Uri(Url)
        };
        _deleteManagerFromSite = deleteManagerFromSite;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _logger.LogInformation("Connected to RabbitMQ");
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _deleteManagerFromSite.ConsumeAsync(_channel);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping RabbitMQ Connection");
        _channel.Close();
        _connection.Close();
        return base.StopAsync(cancellationToken);
    }
}
