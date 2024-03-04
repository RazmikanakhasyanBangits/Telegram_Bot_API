using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Repository.Entity;

public class UserRole : BaseEntity<short>
{
    public string Name { get; set; }
    public ICollection<UserActivityHistory> UserActivities { get; set; }
}

public static class UserRoleConfig
{
    public static void AddUserRole(this ModelBuilder builder)
    {
        _ = builder.Entity<UserRole>(opt =>
        {
            _ = opt.HasKey(x => x.Id);
        });
    }
}