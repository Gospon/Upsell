namespace MSIdentity.Application.Types;

public class IntegrationEvent<T>
{
    public string Name { get; set; }
    public T Data { get; set; }
}
