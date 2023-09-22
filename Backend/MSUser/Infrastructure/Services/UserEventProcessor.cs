using MediatR;
using MSUser.Application.Commands;
using MSUser.Application.Interfaces;
using MSUser.Application.Types;
using System.Text.Json;

namespace MSUser.Infrastructure.Services;

public class UserEventProcessor : IUserEventProcessor
{
    private readonly IServiceProvider _provider;

    public UserEventProcessor(IServiceProvider provider)
    {
        _provider = provider;
    }

    public async void Process(string message)
    {
        var jsonDocument = JsonDocument.Parse(message);
        var eventType = jsonDocument.RootElement.GetProperty("Name").GetString();
        var userData = jsonDocument.RootElement.GetProperty("Data").ToString();

        using (var scope = _provider.CreateScope())
        {
            switch (eventType)
            {
                case nameof(UserEventType.UserRegistered):

                    var userDto = JsonSerializer.Deserialize<Domain.Entities.User>(userData);

                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new CreateUserCommand(userDto));
                    break;
            }

        }
    }
}
