namespace Crautnot.Models.DTO;

public class TokenDataDto : TokenExchangeDto {
    public decimal ClosingPrice { get; set; }
    public decimal HighestPrice { get; set; }
    public decimal LowestPrice { get; set; }
    public decimal OpeningPrice { get; set; }
    public DateTime Date { get; set; }
}