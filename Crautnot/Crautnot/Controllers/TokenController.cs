using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Crautnot.Client;
using Crautnot.Client.Responses;
using Crautnot.Models;
using Crautnot.Models.DTO;
using Crautnot.Services;
using Crautnot.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using TL;

namespace Crautnot.Controllers
{
    [Route("token")]
    public class TokenController : Controller {
        private readonly MexcClientService _mexcClientService;
        private readonly WTelegram.Client telegramClient;
        private readonly IMemoryCache _cache;

        private const string RecvWindow = "5000";

        public TokenController(MexcClientService mexcClientService, WTelegram.Client telegramClient, IMemoryCache cache) {
            _mexcClientService = mexcClientService;
            this.telegramClient = telegramClient;
            _cache = cache;
        }

        [HttpGet]
        [Route("exchanges")]
        [ProducesResponseType(typeof(IList<TokenExchangeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> TokenExchanges() {
            return Ok(/*await _tokenService.GetTokenExchanges()*/);
        }

        [HttpGet]
        [Route("exchangeToken/{exchangeTokenId}/data")]
        [ProducesResponseType(typeof(IList<TokenDataDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> TokenData(long exchangeTokenId) {
            return Ok(/*await _tokenService.TokenData(exchangeTokenId)*/);
        }

        [HttpGet]
        [Route("price-information/{exchangeTokenId}")]
        public async Task<IActionResult> TokenPriceInformation(long exchangeTokenId) {
            return Ok(/*await _tokenService.TokenPriceInformation(exchangeTokenId)*/);
        }

        [HttpGet]
        [Route("market-data")]
        [ProducesResponseType(typeof(IList<MarketDataResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> MarketData([FromQuery] string token, [FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int interval) {
            return Ok(/*await _mexcClientService.GetMarketData(token, start, end, interval)*/);
        }

        [HttpPost("test-order")]
        public async Task<IActionResult> CreateOrder() {
            return Ok();
        }

        private async Task<BybitMarkPriceKlineResponse> GetMarkPriceKline(string token) {
            var client = new RestClient("https://api-demo.bybit.com/");
            var request = new RestRequest($"v5/market/mark-price-kline?symbol={token}&interval=1&limit=1");

            var response = await client.GetAsync<BybitMarkPriceKlineResponse>(request);

            return response;
        }

        private string GeneratePostSignature(IDictionary<string, object> parameters, string timestamp) {
            string paramJson = JsonConvert.SerializeObject(parameters);
            string rawData = timestamp + "yiimOPvKTAjFImXg8i" + RecvWindow + paramJson;

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("CgQodZClpXxM0SxjCpo1Hw23aK64BDPYlvPg"));
            var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return BitConverter.ToString(signature).Replace("-", "").ToLower();
        }
    }
}
