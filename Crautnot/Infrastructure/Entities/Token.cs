using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities
{
    public class Token : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ExchangeTokens> ExchangeTokens { get; set; } = new List<ExchangeTokens>();   
    }

    internal sealed class TokenEntityTypeConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd(); // Auto-increment

            builder.Property(x => x.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.HasMany(x => x.ExchangeTokens)
                   .WithOne(x => x.Token)
                   .HasForeignKey(x => x.TokenId)
                   .HasPrincipalKey(e => e.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
