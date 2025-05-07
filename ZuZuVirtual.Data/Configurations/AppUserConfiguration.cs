using ZuZuVirtual.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZuZuVirtual.data.Configurations
{
    class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasColumnType("Varchar(50)").HasMaxLength(50);
            builder.Property(x => x.SurName).IsRequired().HasColumnType("Varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasColumnType("Varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Phone).HasColumnType("Varchar(15)").HasMaxLength(15);
            builder.Property(x => x.Password).IsRequired().HasColumnType("nVarchar(50)").HasMaxLength(50);
            builder.Property(x => x.UserName).HasColumnType("Varchar(50)").HasMaxLength(50);
            builder.HasData(
                new AppUser
                {
                    Id = 1,
                    UserName = "Admin",
                    Email = "admin@vurtalmarket.io",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "Test",
                    SurName = "User",
                    Password = "123456*",

                }
                );
        }
    }
}

//An EF Core configuration class that defines how to represent and add initial data in the databse