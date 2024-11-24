namespace Crautnot.Models.DTO; 

public class BybitNewsDto {
    public string Token { get; set; }
    public DateTime ListingDate { get; set; }
    public DateTime PublishDate { get; set; }
    public decimal HighestPriceWhenPublishedNews { get; set; }

    public bool IsExistOnMexc { get; set; }
    public decimal HighestPriceOnMexc { get; set; }
    public DateTime HighestPriceDateOnMexc { get; set; }
    public decimal LowestPriceOnMexc { get; set; }
    public DateTime LowestPriceDateOnMexc { get; set; }

    public bool IsExistOnGateIo { get; set; }
    public decimal HighestPriceOnGateIo { get; set; }
    public DateTime HighestPriceDateOnGateIo { get; set; }
    public decimal LowestPriceOnGateIo { get; set; }
    public DateTime LowestPriceDateOnGateIo { get; set; }
}