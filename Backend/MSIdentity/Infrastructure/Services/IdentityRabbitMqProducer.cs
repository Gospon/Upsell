using MSIdentity.Infrastructure.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MSIdentity.Infrastructure.Services;

public class IdentityRabbitMqProducer : IIdentityRabbitMqProducer
{
    private readonly IConfiguration _configuration;
    public IdentityRabbitMqProducer(IConfiguration configuration) { _configuration = configuration; }
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:RabbitMQHost"],
            Port = _configuration.GetValue<int>("RabbitMQ:RabbitMQPort")
        };

        var connection = factory.CreateConnection();
        using
        var channel = connection.CreateModel();

        channel.QueueDeclare("identity", exclusive: false);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "", routingKey: "identity", body: body);
    }
}