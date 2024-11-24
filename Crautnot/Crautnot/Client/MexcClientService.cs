using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Crautnot.Client.Responses;
using RestSharp;

namespace Crautnot.Client; 

public class MexcClientService : AbstractClientService {
    private const string MexcBaseUrl = "https://contract.mexc.com/";

    public async Task<List<MarketDataResponse>> GetMarketData(string token, DateTime start, DateTime end, int interval) {
        var client = new RestClient(MexcBaseUrl);

        var startLong = ConvertDateToUnxTime(start, true);
        var endLong = ConvertDateToUnxTime(end, true);

        var request =
            new RestRequest($"api/v1/contract/kline/{token}_USDT?interval=Min{interval}&start={startLong}&end={endLong}");

        try {
            var response = await client.GetAsync<MexcFuturesMarketDataResponse>(request);
            var marketDataList = new List<MarketDataResponse>();

            if (!response.Success) {
                return marketDataList;
            }
            for (var i = 0; i <= response.Data.Time.Count - 1; i++) {
                var marketData = new MarketDataResponse {
                    DateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(response.Data.Time[i].ToString())).DateTime,
                    OpenPrice = decimal.Parse(response.Data.Open[i].ToString()),
                    HighPrice = decimal.Parse(response.Data.High[i].ToString()),
                    LowPrice = decimal.Parse(response.Data.Low[i].ToString()),
                    ClosePrice = decimal.Parse(response.Data.Close[i].ToString())
                };

                marketDataList.Add(marketData);
            }

            return marketDataList;
        }
        catch (Exception e) {
            return new List<MarketDataResponse>();
        }
    }

    public async Task<bool> IsTokenExist(string token) {
        var client = new RestClient(MexcBaseUrl);

        var request = new RestRequest($"api/v1/contract/detail?symbol={token}_USDT");

        var response = await client.ExecuteAsync(request);

        return response.IsSuccessStatusCode;
    }
}