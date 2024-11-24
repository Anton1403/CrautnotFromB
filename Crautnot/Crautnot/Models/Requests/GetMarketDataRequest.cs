namespace Crautnot.Models.Requests; 

public class GetMarketDataRequest {
    public string Token { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int Interval { get; set; }
}