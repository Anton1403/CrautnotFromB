using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Dtos;
using Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities
{
    public class News : IEntity
    {
        public long Id { get; set; }
        public string Topic { get; set; }
        public DateTime ListingDate { get; set; }
        public DateTime PublishDate { get; set; }
        public CategoryEnum Category { get; set; }

        public List<ExchangeTokens> ExchangeTokens { get; set; }
    }

    internal sealed class NewsEntityTypeConfiguration : IEntityTypeConfiguration<News> {
        public void Configure(EntityTypeBuilder<News> builder) {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd(); // Auto-increment

            builder.Property(x => x.Topic).HasMaxLength(255).IsRequired();
            builder.Property(x => x.ListingDate).IsRequired();
            builder.Property(x => x.PublishDate).IsRequired();
            builder.Property(x => x.Category).IsRequired();

            builder.HasMany(x => x.ExchangeTokens)
                   .WithMany(x => x.News);
        }
    }
}
