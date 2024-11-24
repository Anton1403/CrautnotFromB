using Crautnot.Models.Requests;
using Crautnot.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crautnot.Controllers;

[Route("news")]
public class NewsController : Controller {
    private readonly NewsService _newsService;

    public NewsController(NewsService newsService) {
        _newsService = newsService;
    }

    /// <summary>
    /// Tag:
    /// Spot = 0,
    /// Derivatives = 1
    /// BybitCategory: 
    /// new_crypto = 0,
    /// delistings = 1
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("latest-bybit-news")]
    public async Task<IActionResult> CheckLatestListingNewsOnBybit(CheckLatestListingNewsOnBybitRequest request) {
        return Ok(await _newsService.GetLatestListingNewsOnBybit(request));
    }
}