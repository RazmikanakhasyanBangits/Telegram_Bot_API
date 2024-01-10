namespace Core.Services.Bot.Abstraction;

public interface ICommandHandler
{
    void ReStartBot();
    void StartBot();
    void StopBot();
}
