using Crautnot.Client.Responses;
using Crautnot.Models;
using Crautnot.Models.Requests;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;
using Crautnot.Services;

namespace Crautnot.Client;

public class BybitClientService : AbstractClientService {
    private readonly ExchangeOptions exchangeOptions;
    private readonly ErrorNotifier _notifier;

    private const int takeProfitPercentage = 100;
    private const int stopLossPercentage = 50;
    private const string RecvWindow = "5000";

    public BybitClientService(IOptions<ExchangeOptions> exchangeOptions, ErrorNotifier notifier) {
        _notifier = notifier;
        this.exchangeOptions = exchangeOptions.Value;
    }

    public async Task<BybitMainResponse.BybitResponse> CheckBybitAnnouncement(CheckLatestListingNewsOnBybitRequest requestDto) {
        var client = new RestClient(exchangeOptions.Bybit.Url);
        client.AddDefaultHeader(exchangeOptions.Bybit.Key, exchangeOptions.Bybit.Secret);

        var request = new RestRequest($"v5/announcements/index?locale=en-US&type={requestDto.Category}&limit={requestDto.Limit ?? 1}");

        var response = await client.GetAsync<BybitMainResponse.BybitResponse>(request);

        return response;
    }

    public async Task CreateOrder(string token, bool isLong) {
        var markPrice = await GetMarkPriceKline(token);
        _notifier.Notify(new Exception($"{markPrice.RetCode}. {markPrice.RetMsg}"));
        if(markPrice.Result.List == null) return;
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var price = Convert.ToDecimal(markPrice.Result.List.FirstOrDefault()[1]);
        var amountWithLeverage = 300;
        var stopLoss = isLong
            ? price - price * stopLossPercentage / 1000
            : price + price * stopLossPercentage / 1000;
        var takeProfit = isLong
        ? price + price * takeProfitPercentage / 1000
        : price - price * takeProfitPercentage / 1000;
        var side = isLong
            ? "Buy"
            : "Sell";
        var quantity = (int)Math.Floor(amountWithLeverage / price);

        using var client = new HttpClient(); 
        var parameters = new Dictionary<string, object> {
            {"category", "linear"},
            {"symbol", $"{token}"},
            {"side", side},
            {"positionIdx", 0},
            {"orderType", "Market"},
            {"qty", $"{quantity}"},
            {"timeInForce", "GTC"},
            //{"takeProfit", $"{takeProfit}"},
            //{"tpslMode","Full"}
        };
        var signature = GeneratePostSignature(parameters, timestamp);
        var jsonPayload = JsonConvert.SerializeObject(parameters);
        
        HttpRequestMessage request = new(HttpMethod.Post, $"{exchangeOptions.Bybit.Url}v5/order/create") {
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };

        request.Headers.Add("X-BAPI-API-KEY", exchangeOptions.Bybit.Key);
        request.Headers.Add("X-BAPI-SIGN", signature);
        request.Headers.Add("X-BAPI-SIGN-TYPE", "2");
        request.Headers.Add("X-BAPI-TIMESTAMP", timestamp);
        request.Headers.Add("X-BAPI-RECV-WINDOW", RecvWindow);


        var response = client.SendAsync(request).Result;
        _notifier.Notify(new Exception($"{response.Content.ReadAsStringAsync().Result}"));

        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
    }

    public async Task<BybitMarkPriceKlineResponse> GetMarkPriceKline(string token) {
        var client = new RestClient(exchangeOptions.Bybit.Url);
        var request = new RestRequest($"v5/market/mark-price-kline?symbol={token}&interval=1&limit=1");

        var response = await client.GetAsync<BybitMarkPriceKlineResponse>(request);

        return response;
    }

    private string GeneratePostSignature(IDictionary<string, object> parameters, string timestamp) {
        string paramJson = JsonConvert.SerializeObject(parameters);
        string rawData = timestamp + exchangeOptions.Bybit.Key + RecvWindow + paramJson;

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(exchangeOptions.Bybit.Secret));
        var signature = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return BitConverter.ToString(signature).Replace("-", "").ToLower();
    }
}