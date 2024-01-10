using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository.Entity;

public class UserStatus : BaseEntity<short>
{
    public string Name { get; set; }
    public ICollection<UserActivityHistory> UserActivities { get; set; }
}
public static class UserStatusConfig
{
    public static void AddUserStatus(this ModelBuilder builder)
    {
        _ = builder.Entity<UserStatus>(opt =>
        {
            _ = opt.HasKey(x => x.Id);
        });
    }
}