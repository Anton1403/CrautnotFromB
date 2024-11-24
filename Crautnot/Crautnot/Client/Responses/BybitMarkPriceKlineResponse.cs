namespace Crautnot.Client.Responses; 

public class BybitMarkPriceKlineResponse {
    public int RetCode { get; set; }
    public string RetMsg { get; set; }
    public Result Result { get; set; }
}

public class Result {
    public List<List<string>> List { get; set; }
}