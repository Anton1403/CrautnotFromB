using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Infrastructure.Dtos;

public class NewsDto {
    public long Id { get; set; }
    public string Topic { get; set; }
    public DateTime ListingDate { get; set; }
    public DateTime PublishDate { get; set; }
    public CategoryEnum Category { get; set; }
    public List<ExchangeTokens> ExchangeTokens { get; set; }
}