namespace Crautnot.Models.DTO; 

public class TokenExchangeDto {
    public long Id { get; set; }
    public string Exchange { get; set; }
    public string Token { get; set; }
    public bool IsLong { get; set; }
}