namespace Crautnot.Client.Responses; 

public class MexcFuturesMarketDataResponse {
    public bool Success { get; set; }
    public int Code { get; set; }
    public Data Data { get; set; }
}

public class Data {
    public List<int> Time { get; set; }
    public List<double> Open { get; set; }
    public List<double> Close { get; set; }
    public List<double> High { get; set; }
    public List<double> Low { get; set; }
    public List<double> Vol { get; set; }
    public List<double> Amount { get; set; }
    public List<double> RealOpen { get; set; }
    public List<double> RealClose { get; set; }
    public List<double> RealHigh { get; set; }
    public List<double> RealLow { get; set; }
}