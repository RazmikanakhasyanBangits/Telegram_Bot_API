// Ignore Spelling: Bio

using Microsoft.EntityFrameworkCore;
using Repository.Entity;
using System;
using System.Collections.Generic;

namespace Repository.Entity;

public class UserActivityHistory
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short StatusId { get; set; }
    public UserStatus Status { get; set; }
    public short RoleId { get; set; }
    public UserRole Role { get; set; }
    public long UserExternalId { get; set; }
    public string Bio { get; set; }
    public string Description { get; set; }
    public ICollection<ChatDetail> ChatDetails { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}

public static class UserActivityHistoryConfig
{
    public static void AddUserActivity(this ModelBuilder builder)
    {
        _ = builder.Entity<UserActivityHistory>(opt =>
        {
            _ = opt.HasKey(x => x.Id);

            _ = opt.Property(x => x.StatusId).HasDefaultValue(1);
            _ = opt.Property(x => x.RoleId).HasDefaultValue(2);
            _ = opt.HasOne(x => x.Status)
            .WithMany(x => x.UserActivities)
            .HasForeignKey(x => x.StatusId);

            _ = opt.HasOne(x => x.Role)
            .WithMany(x => x.UserActivities)
            .HasForeignKey(x => x.RoleId);
        });
    }
}
