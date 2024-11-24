using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crautnot.Client.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace Crautnot.Client
{
    public class GateIoClientService : AbstractClientService {
        private const string BaseUrl = "https://api.gateio.ws/";

        public async Task<bool> IsFuturesTokenExist(string token) {
            var client = new RestClient(BaseUrl);

            var request = new RestRequest($"api/v4/futures/usdt/contracts/{token}_USDT");

            var response = await client.ExecuteAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsSpotTokenExist(string token) {
            var client = new RestClient(BaseUrl);

            var request = new RestRequest($"api/v4/spot/currencies/{token}");

            var response = await client.ExecuteAsync(request);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MarketDataResponse>> GetMarketData(string token, DateTime start, DateTime end, int interval) {
            var client = new RestClient(BaseUrl);

            var startLong = ConvertDateToUnxTime(start, true);
            var endLong = ConvertDateToUnxTime(end, true);

            var request = new RestRequest($"api/v4/futures/usdt/candlesticks?contract={token}_USDT&from={startLong}&to={endLong}&interval={interval}m");

            try {
                var executedResponse = await client.ExecuteAsync(request);
                var marketDataList = new List<MarketDataResponse>();
                if (!executedResponse.IsSuccessStatusCode) {
                    return new List<MarketDataResponse>();
                }

                var response = JsonConvert.DeserializeObject<List<GateIoFuturesMarketDataResponse>>(executedResponse.Content);
                foreach(var item in response) {
                    var marketData = new MarketDataResponse {
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(item.T.ToString())).DateTime,
                        OpenPrice = decimal.Parse(item.O),
                        HighPrice = decimal.Parse(item.H),
                        LowPrice = decimal.Parse(item.L),
                        ClosePrice = decimal.Parse(item.C)
                    };

                    marketDataList.Add(marketData);
                }

                return marketDataList;
            }
            catch (Exception e) {
                return new List<MarketDataResponse>();
            }
        }
    }
}
