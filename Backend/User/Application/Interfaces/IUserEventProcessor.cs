namespace User.Application.Interfaces;

public interface IUserEventProcessor
{
    public void Process(string message);
}
