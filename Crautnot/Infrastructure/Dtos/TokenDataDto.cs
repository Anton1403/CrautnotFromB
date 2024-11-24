using Infrastructure.Enums;

namespace Infrastructure.Dtos;

public class TokenDataDto {
    public DateTime Dtv { get; set; }
    public decimal TradingVolume { get; set; }
    public decimal ClosingPrice { get; set; }
    public decimal HighestPrice { get; set; }
    public decimal LowestPrice { get; set; }
    public decimal OpeningPrice { get; set; }
}