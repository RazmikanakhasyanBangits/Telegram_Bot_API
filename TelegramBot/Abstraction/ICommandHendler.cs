using System.Threading.Tasks;

namespace TelegramBot.Abstraction
{
    internal interface ICommandHendler
    {
        Task HandleAsync(string command);
    }
}
