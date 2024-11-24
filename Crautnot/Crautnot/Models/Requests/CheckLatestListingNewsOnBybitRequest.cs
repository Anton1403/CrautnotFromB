using Crautnot.Models.Enums;

namespace Crautnot.Models.Requests; 

public class CheckLatestListingNewsOnBybitRequest {
    public int? Limit { get; set; }
    public BybitTag Tag { get; set; }
    public BybitCategory Category { get; set; }
}