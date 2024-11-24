using Crautnot.Client.Responses;
using Crautnot.SignalR;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RestSharp;

namespace Crautnot.Client
{
    public class BinanceClientService(IHubContext<QuartzHub> hubContext) {
        private static string BaseUrl = "https://www.binance.com/";

        public async Task<BinanceAnnouncementResponse> CheckBinanceAnnouncement(int limit, int? categoryId) {
            var client = new RestClient(BaseUrl);
            var requestUrl = $"/bapi/composite/v1/public/cms/article/list/query?type=1&pageSize={limit}&pageNo=1";
            if (categoryId != null) {
                requestUrl += $"&catalogId={categoryId}";
            }
            var request =
                new RestRequest(requestUrl);

            request.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            request.AddHeader("Pragma", "no-cache");
            request.AddHeader("Expires", "0");
            var time = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss");
            var res = await client.ExecuteAsync(request);

            await hubContext.Clients.All.SendAsync("RequestUrl", requestUrl);
            await hubContext.Clients.All.SendAsync("Time", time);
            await hubContext.Clients.All.SendAsync("RequestDate", res.Headers.FirstOrDefault(x => x.Name == "Date").Value);

            return JsonConvert.DeserializeObject<BinanceAnnouncementResponse>(res.Content);
        }
    }
}
