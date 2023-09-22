namespace MSIdentity.Infrastructure.Interfaces;

public interface IIdentityRabbitMqProducer
{
    public void SendMessage<T>(T message);
}
