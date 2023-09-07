namespace Identity.Application.Interfaces;

public interface IIdentityRabbitMqProducer
{
    public void SendMessage<T>(T message);
}
