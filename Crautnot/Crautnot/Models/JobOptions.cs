namespace Crautnot.Models; 

public class JobOptions {
    public bool SyncJob { get; set; } = true;
    public bool CalculateDealLiquidationJob { get; set; }
    public bool StoreTokenDataJob { get; set; }
}