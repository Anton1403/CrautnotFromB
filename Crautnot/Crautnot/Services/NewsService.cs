using Crautnot.Client;
using Crautnot.Extensions;
using Crautnot.Models.DTO;
using Crautnot.Models.Requests;
using Quartz.Util;

namespace Crautnot.Services
{
    public class NewsService : AbstractService {
        private readonly BybitClientService _bybitClientService;
        private readonly MexcClientService _mexcClientService;
        private readonly GateIoClientService _gateIoClientService;

        public NewsService(BybitClientService bybitClientService, MexcClientService maexcClientService, GateIoClientService gateIoClientService) {
            _bybitClientService = bybitClientService;
            _mexcClientService = maexcClientService;
            _gateIoClientService = gateIoClientService;
        }

        public async Task<List<BybitNewsDto>> GetLatestListingNewsOnBybit(CheckLatestListingNewsOnBybitRequest request) {
            var response = await _bybitClientService.CheckBybitAnnouncement(request);
            var result = new List<BybitNewsDto>();

            foreach (var news in response.Result.List) {
                var token = GetTokenName(news.Title);
                if (token.IsNullOrWhiteSpace()) {
                    result.Add(new BybitNewsDto {
                        Token = news.Title
                    });
                    continue;
                }
                var publishDate = ConvertDate.ConvertStringToDateTime(news.PublishTimestamp.ToString());

                var bybitResult = new BybitNewsDto {
                    Token = token,
                    PublishDate = publishDate,
                    ListingDate = ConvertDate.ConvertStringToDateTime(news.StartDateTimestamp.ToString()),
                };

                var mexcData =
                    await _mexcClientService.GetMarketData(token, publishDate, publishDate.AddDays(1), 5);
                var gateIoData = await _gateIoClientService.GetMarketData(token, publishDate, publishDate.AddDays(1), 5);

                if (mexcData.Any()) {
                    bybitResult.HighestPriceWhenPublishedNews = mexcData.Select(x => x.HighPrice).First();

                    var highestItem = mexcData.OrderByDescending(x => x.HighPrice).First();
                    bybitResult.HighestPriceOnMexc = highestItem.HighPrice;
                    bybitResult.HighestPriceDateOnMexc = highestItem.DateTime;

                    var lowestItem = mexcData.OrderBy(x => x.LowPrice).First();
                    bybitResult.LowestPriceOnMexc = lowestItem.LowPrice;
                    bybitResult.LowestPriceDateOnMexc = lowestItem.DateTime;

                    bybitResult.IsExistOnMexc = true;
                }
                if(gateIoData.Any()) {
                    if (bybitResult.HighestPriceWhenPublishedNews == 0) {
                        bybitResult.HighestPriceWhenPublishedNews = gateIoData.Select(x => x.HighPrice).First();
                    }

                    var highestItem = gateIoData.OrderByDescending(x => x.HighPrice).First();
                    bybitResult.HighestPriceOnGateIo = highestItem.HighPrice;
                    bybitResult.HighestPriceDateOnGateIo = highestItem.DateTime;

                    var lowestItem = gateIoData.OrderBy(x => x.LowPrice).First();
                    bybitResult.LowestPriceOnGateIo = lowestItem.LowPrice;
                    bybitResult.LowestPriceDateOnGateIo = lowestItem.DateTime;

                    bybitResult.IsExistOnGateIo = true;
                }

                result.Add(bybitResult);
            }

            return result;
        }
    }
}
