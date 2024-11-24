using Crautnot.Client.Responses;

namespace Crautnot.Models.DTO; 

public class ArticleWithCategory {
    public Article Article { get; set; }
    public int CategoryId { get; set; }
}