using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using Service.Model.Enum;

namespace Repository;

public static class DbContextExtensions
{
    public static void Seed(this ModelBuilder builder)
    {
        _ = builder.Entity<UserStatus>().HasData(
          new UserStatus { Id = 1, Name = UserStatusEnum.Active.ToString() },
          new UserStatus { Id = 2, Name = UserStatusEnum.Blocked.ToString() });

        _ = builder.Entity<UserRole>().HasData(
         new UserRole { Id = 1, Name = UserRoleEnum.Admin.ToString() },
         new UserRole { Id = 2, Name = UserRoleEnum.User.ToString() });
    }
}
