using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Core.Services.Interfaces;

public interface IUserActivityHistoryService
{
    Task AddChatHistory(MessageEventArgs request, string response);
    Task<bool> BlockUserAsync(string userName);
    Task<bool> IsUserBlockedAsync(long userId);
    Task<bool> UnBlockUserAsync(string userName);
}
