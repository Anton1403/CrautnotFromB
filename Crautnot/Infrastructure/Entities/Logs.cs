using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities; 

public class Logs : IEntity {
    public long Id { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
    public DateTime ResponseDate { get; set; }
    public int Status { get; set; }
}

internal sealed class LogsEntityTypeConfiguration : IEntityTypeConfiguration<Logs> {
    public void Configure(EntityTypeBuilder<Logs> builder) {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd(); // Auto-increment
        builder.Property(x => x.Message).IsRequired();
        builder.Property(x => x.Date).IsRequired();
        builder.Property(x => x.ResponseDate);
        builder.Property(x => x.Status);
    }
}