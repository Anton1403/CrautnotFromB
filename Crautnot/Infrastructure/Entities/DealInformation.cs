using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities; 

public class DealInformation : IEntity {
    public long Id { get; set; }
    public long ExchangeTokenId { get; set; }
    public int MaxLeverage { get; set; }
    public bool IsLong { get; set; }

    public virtual ExchangeTokens ExchangeToken { get; set; }
}

internal sealed class DealInformationEntityTypeConfiguration : IEntityTypeConfiguration<DealInformation> {
    public void Configure(EntityTypeBuilder<DealInformation> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExchangeTokenId).IsRequired();
        builder.Property(x => x.MaxLeverage).IsRequired();
        builder.Property(x => x.IsLong).IsRequired();

        builder.HasOne(x => x.ExchangeToken)
               .WithMany()
               .HasForeignKey(x => x.ExchangeTokenId);
    }
}