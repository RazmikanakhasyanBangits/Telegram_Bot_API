using Core.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enum;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace Core.Services.Implementations;

public class UserActivityHistoryService : IUserActivityHistoryService
{

    private readonly IUserActivityHistoryRepository _historyRepository;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IChatDetailRepository _chatDetailRepository;

    public UserActivityHistoryService(IUserActivityHistoryRepository historyRepository,
        IChatDetailRepository chatDetailRepository, IServiceScopeFactory scopeFactory)
    {
        _historyRepository = historyRepository;
        _chatDetailRepository = chatDetailRepository;
        _scopeFactory=scopeFactory;
    }

    public async Task AddChatHistory(MessageEventArgs request, string response)
    {
        UserActivityHistory history = await _historyRepository.GetDetailsAsync(x => x.UserExternalId == request.Message.From.Id,
            includes: i => i.Include(x => x.ChatDetails));

        if (history is null)
        {
            UserActivityHistory userActivityHistory =
            await _historyRepository.AddAsync(new()
            {
                UserName = request.Message.Chat.Username,
                LastName = request.Message.Chat.LastName,
                FirstName = request.Message.Chat.FirstName,
                Bio = request.Message.Chat.Bio,
                Description = request.Message.Chat.Description,
                UserExternalId = request.Message.From.Id,
                CreationDate = DateTime.UtcNow,
                LastUpdateDate = DateTime.UtcNow,
            });

            _ = await _chatDetailRepository.AddAsync(new()
            {
                CreationDate = DateTime.UtcNow,
                Message = request.Message.Text,
                MessageExternalId = request.Message.MessageId,
                Response = response,
                UserActivityHistoryId = userActivityHistory.Id,
            });
            return;
        }


        history.UserName = request.Message.Chat.Username;
        history.LastName = request.Message.Chat.LastName;
        history.FirstName = request.Message.Chat.FirstName;
        history.Bio = request.Message.Chat.Bio;
        history.Description = request.Message.Chat.Description;
        history.ChatDetails.Add(new()
        {
            CreationDate = DateTime.UtcNow,
            Message = request.Message.Text,

            MessageExternalId = request.Message.MessageId,
            Response = response,
            UserActivityHistoryId = history.Id,
        });
        history.LastUpdateDate = DateTime.UtcNow;

        await _historyRepository.UpdateAsync(history);

    }

    public async Task<bool> BlockUserAsync(string userName)
    {
        try
        {
            var user = await _historyRepository.GetDetailsAsync(x => x.UserExternalId.ToString()==userName);
            user.StatusId=(short)UserStatusEnum.Blocked;
            user.LastUpdateDate=DateTime.UtcNow;

            await _historyRepository.UpdateAsync(user);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UnBlockUserAsync(string userName)
    {
        try
        {
            var user = await _historyRepository.GetDetailsAsync(x => x.UserExternalId.ToString()==userName);
            user.StatusId=(short)UserStatusEnum.Active;
            user.LastUpdateDate=DateTime.UtcNow;
            await _historyRepository.UpdateAsync(user);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> IsUserBlockedAsync(long userId)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var historyRepository = scopedServices.GetRequiredService<IUserActivityHistoryRepository>();
            var user = await historyRepository.GetDetailsAsync(x => x.UserExternalId==userId);
            if (user is null) return false;
            return user.StatusId == (short)UserStatusEnum.Blocked;
        }
    }
}
