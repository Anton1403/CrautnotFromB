using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    public class Exchange
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExchangeTokens> ExchangeTokens { get; set; } = new List<ExchangeTokens>();   
    }

    internal sealed class ExchangeEntityTypeConfiguration : IEntityTypeConfiguration<Exchange>
    {
        public void Configure(EntityTypeBuilder<Exchange> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // Auto-increment

            builder.Property(x => x.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.HasMany(x => x.ExchangeTokens)
                   .WithOne(x => x.Exchange)
                   .HasForeignKey(x => x.ExchangeId)
                   .HasPrincipalKey(e => e.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
