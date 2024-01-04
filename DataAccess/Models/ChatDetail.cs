// Ignore Spelling: Ef
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Models;

public class ChatDetail
{
    public long Id { get; set; }
    public string Message { get; set; }
    public string Response { get; set; }
    public DateTime CreationDate { get; set; }
    public long MessageExternalId { get; set; }
    public long UserActivityHistoryId { get; set; }
    public UserActivityHistory UsersActivityHistory { get; set; }
}

public static class ChatDetailEfConfig
{
    public static void ConfigChatDetails(this ModelBuilder builder)
    {
        _ = builder.Entity<ChatDetail>(opt =>
        {
            _ = opt.HasIndex(x => x.Id);
            _ = opt.HasOne(x => x.UsersActivityHistory)
            .WithMany(x => x.ChatDetails)
            .HasForeignKey(x => x.UserActivityHistoryId);
        });
    }
}