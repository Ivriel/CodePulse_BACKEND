using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "59d36203-8dc9-4d21-bd70-8ddc231800c9";
            var writerRoleId = "0e76379e-e94c-40b1-b0c2-213e73e91ccf";

            // Create reader and writer roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = "4d6dcf4d-a2bd-405a-8a99-69108ccc8f01";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEKgaO7meRCnZTSC1JVQVSSPI4l+jz7k4zqAwP4JOp+BOu5Gq1Ovd+7IUOojhXVSW4Q==", // Pre-computed hash for "Admin@123"
                SecurityStamp = "7c32d8af-e09e-49e5-8802-63d860cd9744",
                ConcurrencyStamp = "74f5045a-04a4-48f6-b07f-dcca433823d0"
            };

            builder.Entity<IdentityUser>().HasData(admin);

            // Give roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}