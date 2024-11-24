using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Crautnot.Client;
using Crautnot.Client.Responses;
using Crautnot.Telegram;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crautnot.Extensions;
using Crautnot.Models.DTO;
using Crautnot.Models.Enums;
using Crautnot.Models.Requests;
using Quartz.Util;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Caching.Memory;
using TL;
using WTelegram;
using Crautnot.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;

namespace Crautnot.Services
{
    public class SynchronizeService : AbstractService {
        private readonly BybitClientService bybitClientService;
        private readonly MexcClientService mexcClientService;
        private readonly GateIoClientService gateIoClientService;
        private readonly BinanceClientService binanceClientService;
        private readonly OkxClientService okxClientService;
        private readonly IHubContext<QuartzHub> hubContext;
        private readonly IMemoryCache _cache;

        private readonly TelegramBot telegramBot;

        private const string PatternOneWord = @"(\w+)USDT";
        private const string PatternSlash = @"(\w+)/USDT";

        private static readonly int apiId = 21411349;
        private static readonly string apiHash = "7506e81ddd2b4e0495100b04febf3aa7";
        private static readonly string phoneNumber = "+375298884084";

        private static readonly List<string> LongTitles = new() {"Will Launch", "Will List"};
        private static readonly List<string> ShortTitles = new(){ "Will Delist" };

        public SynchronizeService(BybitClientService bybitClientService, 
            TelegramBot telegramBot, 
            MexcClientService mexcClientService, 
            GateIoClientService gateIoClientService, 
            BinanceClientService binanceClientService, 
            OkxClientService okxClientService, 
            IHubContext<QuartzHub> hubContext, IMemoryCache cache) {
            this.bybitClientService = bybitClientService;
            this.telegramBot = telegramBot;
            this.mexcClientService = mexcClientService;
            this.gateIoClientService = gateIoClientService;
            this.binanceClientService = binanceClientService;
            this.okxClientService = okxClientService;
            this.hubContext = hubContext;
            _cache = cache;
        }

        public async Task Synchronize(int limit, int? categoryId) {
            await SynchronizeBinance(limit, categoryId);
        }

        private async Task SynchronizeBinance(int limit, int? categoryId) {
            var response = await binanceClientService.CheckBinanceAnnouncement(limit, categoryId);
            if (!response.Success) {
                throw new Exception(response.Message.ToString());
            }

            var listingNews = response.Data.Catalogs
                    .Where(x => x.CatalogId is 48 or 161)
                    .SelectMany(x => x.Articles.Select(article => new ArticleWithCategory
                        { Article = article, CategoryId = x.CatalogId }));
            var mergedNews = listingNews
                .GroupBy(x => x.CategoryId)
                .Select(g => g.OrderByDescending(x => x.Article.ReleaseDate).First())
                .ToList();

            foreach(var news in mergedNews) {
                var title = news.Article.Title;
                var isNewsExists = _cache.TryGetValue(title, out _);

                if (!isNewsExists) {
                    _cache.Set(title, title, new MemoryCacheEntryOptions {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
                    });
                    //var publishDate = ConvertObjectToDateTime(news.Article.ReleaseDate);

                    var utcNow = DateTime.UtcNow;
                    //await OpenOrder(title, ExchangeEnum.Binance, publishDate, utcNow);

                    await telegramBot.Send(GenerateMessageForTelegram(new TelegramMessageDto {
                        Title = title,
                        Url = "https://www.binance.com/en/support/announcement",
                        Date = utcNow
                    }));
                }
            }
        }

        private async Task OpenOrder(string title, ExchangeEnum exchange, DateTime messageDate, DateTime utcNow) {
            if (exchange == ExchangeEnum.Binance) {
                if(LongTitles.Any(title.Contains) && messageDate.AddSeconds(5) > utcNow) {
                    await NewsFromBinance(title, true);
                }
                else if(ShortTitles.Any(title.Contains) && messageDate.AddSeconds(5) > utcNow) {
                    await NewsFromBinance(title, false);
                }
            }
        }

        private async Task NewsFromBinance(string title, bool isLong) {
            var pattern = @"\b[A-Z0-9]+\b";

            if(isLong) {
                if (title.Contains("Will List")) {
                    string patternForListingCoin = @"\((\b[A-Z]{2,}\b)\)";
                    MatchCollection matches = Regex.Matches(title, patternForListingCoin);

                    foreach(Match match in matches) {
                        var token = match.Value.Replace("(", "").Replace(")","");
                        if(!token.EndsWith("USDT")) {
                            token += "USDT";
                        }
                        await bybitClientService.CreateOrder(token, isLong);
                    }
                }
                else {
                    HashSet<string> exclusions = new HashSet<string> { "USD", "FDUSD", "BNB" };
                    MatchCollection matches = Regex.Matches(title, pattern);

                    if(matches.Count > 0) {
                        foreach(Match match in matches) {
                            var token = match.Value;
                            if(!exclusions.Contains(token) && token.Length > 3) {
                                if(!token.EndsWith("USDT")) {
                                    token += "USDT";
                                }
                                await bybitClientService.CreateOrder(token, isLong);
                            }
                        }
                    }
                }
            }
            else {
                var matches = Regex.Matches(title, pattern);
                if(matches.Count > 0)
                    foreach(Match match in matches) {
                        var token = match.Value;
                        if(!token.EndsWith("USDT"))
                            token += "USDT";
                        await bybitClientService.CreateOrder(token, isLong);
                    }
            }
        }

        private string GenerateMessageForTelegram(TelegramMessageDto message) {
            var result = $"<b>BINANCEAPI</b>\n" +
                         $"{message.Title}\n" +
                         $"{message.Date}";
                         //$"<b>Mexc:</b> {(message.IsTokenNameNotFound ? "&#10068;" : (message.IsExistOnMexc ? "&#9989;" : "&#10060;"))}\n" +
                         //$"<b>GateIo:</b> {(message.IsTokenNameNotFound ? "&#10068;" : (message.IsExistOnGateIo ? "&#9989;" : "&#10060;"))}\n";

            if(!string.IsNullOrWhiteSpace(message.Url)) {
                result += $"<a href='{message.Url}'>{message.Url}</a>";
            }

            return result;
        }
    }
}
