using TL;

namespace Crautnot.Services
{
    public class TelegramClientProvider
    {
        private static WTelegram.Client _client;
        private static readonly object _lock = new();

        public static async Task<WTelegram.Client> GetClientAsync() {
            if(_client == null) {
                lock(_lock) {
                    if(_client == null) {
                        _client = new WTelegram.Client(Config);
                        _client.LoginUserIfNeeded().Wait();
                    }
                }
            }
            return _client;
        }

        static string Config(string what) {
            return what switch {
                "api_id" => "23092757",
                "api_hash" => "ab3073747e9eb60c8355f4f3f8eae47d",
                "phone_number" => "+48575728934",
                "verification_code" => "97494",
                _ => null // Телеграм сам запросит остальные данные при необходимости
            };
        }

        public static async Task DisposeClientAsync() {
            if(_client != null) {
                await _client.DisposeAsync();
                _client = null;
            }
        }
    }
}
