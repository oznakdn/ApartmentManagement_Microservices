using Account.Background.ConsumerServices;
using RabbitMQ.Client;

namespace Account.Background;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly ConnectionFactory _factory;
    private readonly CreatedSiteConsumer _createdSiteConsumer;
    private readonly AssignedManagerToSiteConsumer _assignedManagerToSiteConsumer;
    private readonly AssignedResidentToUnitConsumer _assignedResidentToUnitConsumer;
    private readonly AssignedEmployeeToSiteConsumer _assignedEmployeeToSiteConsumer;
    private readonly string Url;
    private IConnection _connection;
    private IModel _channel;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, CreatedSiteConsumer createdSiteConsumer, AssignedManagerToSiteConsumer assignedManagerToSiteConsumer, AssignedResidentToUnitConsumer assignedResidentToUnitConsumer, AssignedEmployeeToSiteConsumer assignedEmployeeToSiteConsumer)
    {
        _logger = logger;
        _configuration = configuration;
        _createdSiteConsumer = createdSiteConsumer;
        _assignedManagerToSiteConsumer = assignedManagerToSiteConsumer;
        _assignedResidentToUnitConsumer = assignedResidentToUnitConsumer;
        _assignedEmployeeToSiteConsumer = assignedEmployeeToSiteConsumer;


        Url = _configuration["RabbitMqOption:ConnectionString"]!;

        _factory = new ConnectionFactory
        {
            Uri = new Uri(Url)
        };
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
            await _createdSiteConsumer.ConsumeAsync(_channel);
            await _assignedManagerToSiteConsumer.ConsumeAsync(_channel);
            await _assignedResidentToUnitConsumer.ConsumeAsync(_channel);
            await _assignedEmployeeToSiteConsumer.ConsumeAsync(_channel);
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
