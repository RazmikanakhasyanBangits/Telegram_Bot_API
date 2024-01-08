using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Core.Services.Interfaces;

public interface IUserActivityHistoryService
{
    Task AddChatHistory(MessageEventArgs request, string response);
    Task<bool> BlockUserAsync(string userName);
    Task<bool> IsUserBlockedAsync(string userName);
    Task<bool> UnBlockUserAsync(string userName);
}
