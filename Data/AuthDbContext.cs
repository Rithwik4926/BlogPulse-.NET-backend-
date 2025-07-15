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

            var readerRoleId = "dbbc691b-fc37-429e-b9ef-2ab8aa0dc6dd";
            var writerRoleId = "489fc689-c24a-4e00-a219-a6a41f592a80";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },  
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "1d8d0be6-9c08-422e-aac6-e54d02853c1f";
            var adminConcurrencyStamp = "330b0ac1-fbd6-4eb4-b2d0-99422f1ac13c";
            var adminSecurityStamp = "306558e1-b45a-41e7-9799-7e96240316d5";
            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper(),
                EmailConfirmed = true,
                ConcurrencyStamp = adminConcurrencyStamp,
                SecurityStamp = adminSecurityStamp
            };
            admin.PasswordHash = "AQAAAAIAAYagAAAAENTJTXhmo8pMLjKJXHTWtjWCl/sn3whi2vpWslju1gJzBYdR8DH+Lwggbk2jy29IKg==";  //Admin@123

            builder.Entity<IdentityUser>().HasData(admin);

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> 
                { 
                    UserId = adminUserId, 
                    RoleId = readerRoleId 
                },
                new IdentityUserRole<string> 
                { 
                    UserId = adminUserId,
                    RoleId = writerRoleId 
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
