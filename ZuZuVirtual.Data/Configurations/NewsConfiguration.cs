using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZuZuVirtual.core.Entities;


namespace ZuZuVirtual.data.Configurations
{
    class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description);
            builder.Property(x => x.Image).HasMaxLength(100);
        }
    }
}

//Class that defines how the news entity will be represented in the databse
