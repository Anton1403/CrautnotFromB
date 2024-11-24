namespace Crautnot.Models.DTO; 

public class TokenInformationDataDto {
    public string Exchange { get; set; }
    public string Token { get; set; }
    public decimal HighPrice { get; set; }
    public DateTime HighPriceDate { get; set; }
    public decimal LowPrice { get; set; }
    public DateTime LowPriceDate { get; set; }
    public int LeverageOfLiquidation { get; set; }
}