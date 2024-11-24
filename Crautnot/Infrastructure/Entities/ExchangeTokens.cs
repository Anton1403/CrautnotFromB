using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    public class ExchangeTokens : IEntity
    {
        public long Id { get; set; }
        public long TokenId { get; set; }
        public long ExchangeId { get; set; }

        public Exchange Exchange { get; set; }
        public Token Token { get; set; }
        public virtual List<TokenData> TokenData { get; set; }
        public virtual List<News> News { get; set; }
    }

    internal sealed class ExchangeTokensEntityTypeConfiguration : IEntityTypeConfiguration<ExchangeTokens>
    {
        public void Configure(EntityTypeBuilder<ExchangeTokens> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd(); // Auto-increment

            builder.HasOne(x => x.Exchange)
               .WithMany(x => x.ExchangeTokens)
               .HasForeignKey(x => x.ExchangeId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Token)
                   .WithMany(x => x.ExchangeTokens)
                   .HasForeignKey(x => x.TokenId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
