namespace Crautnot.Client.Responses; 

public class MarketDataResponse {
    public DateTime DateTime { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal OpenPrice { get; set; }
}