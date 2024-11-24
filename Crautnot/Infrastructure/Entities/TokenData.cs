using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    public class TokenData : IEntity
    {
        public long Id { get; set; }
        public long ExchangeTokensId { get; set; }
        public DateTime Dtv { get; set; }
        public decimal TradingVolume { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal OpeningPrice { get; set; }
        public ExchangeTokens ExchangeTokens { get; set; }
    }

    internal sealed class TokenDataEntityTypeConfiguration : IEntityTypeConfiguration<TokenData>
    {
        public void Configure(EntityTypeBuilder<TokenData> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd(); // Auto-increment

            builder.Property(x => x.Dtv).IsRequired();
            builder.Property(x => x.TradingVolume).IsRequired();
            builder.Property(x => x.ClosingPrice).IsRequired();
            builder.Property(x => x.HighestPrice).IsRequired();
            builder.Property(x => x.LowestPrice).IsRequired();
            builder.Property(x => x.OpeningPrice).IsRequired();

            builder.HasOne(x => x.ExchangeTokens)
                    .WithMany(x => x.TokenData)
                    .HasForeignKey(x => x.ExchangeTokensId)
                    .HasPrincipalKey(e => e.Id)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
