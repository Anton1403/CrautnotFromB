namespace Crautnot.Models;

public class ExchangeOptions {
    public Bybit Bybit { get; set; }
}

public class Bybit {
    public string Url { get; set; }
    public string Key { get; set; }
    public string Secret { get; set; }
}