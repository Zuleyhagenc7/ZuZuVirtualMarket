using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ZuZuVirtual.core.Entities;



namespace ZuZuVirtual.data.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Image).HasMaxLength(50);
            builder.HasData(
                new Category
                {
                    Name = "YemeIcme",
                    Id = 1,
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 1,
                },
                new Category
                {
                    Name = "sebze",
                    Id = 2,
                    IsActive = true,
                    IsTopMenu = true,
                    ParentId = 0,
                    OrderNo = 2,

                }

                );
        }
    }
}
//Class that defines how the category entity will be represented and add initial data in the database