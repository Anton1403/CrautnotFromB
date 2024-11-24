using Crautnot.Models;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Crautnot.Telegram;

public class TelegramBot {
    private readonly TelegramOptions telegramOptions;

    public TelegramBot(IOptions<TelegramOptions> telegramOptions) {
        this.telegramOptions = telegramOptions.Value;
    }

    public async Task Send(string text) {
        if (telegramOptions.Enable) {
            var bot = new TelegramBotClient(telegramOptions.BotToken);
            await bot.SendTextMessageAsync(telegramOptions.ChatId, text, null, ParseMode.Html);
        }
    }
}