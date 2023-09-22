using MSUser.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MSUser.Infrastructure.Services;

public class UserRabbitMqConsumer : BackgroundService
{
    private readonly IUserEventProcessor _userEventProcessor;
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    public UserRabbitMqConsumer(IUserEventProcessor userEventProcessor, IConfiguration configuration)
    {
        _userEventProcessor = userEventProcessor;
        _configuration = configuration;

        InitializeRabbitMQ();
    }

    public void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:RabbitMQHost"],
            Port = _configuration.GetValue<int>("RabbitMQ:RabbitMQPort")
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare("identity", exclusive: false);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _userEventProcessor.Process(message);
        };

        _channel.BasicConsume("identity", autoAck: true, consumer);

        return Task.CompletedTask;
    }
}
