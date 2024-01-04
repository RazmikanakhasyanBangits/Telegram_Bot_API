using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Core.Services.Interfaces;

public interface IUserActivityHistoryService
{
    [System.Obsolete]
    Task AddChatHistory(MessageEventArgs request, string response);
}
