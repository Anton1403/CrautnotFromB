using Crautnot.Client.Responses;
using RestSharp;

namespace Crautnot.Client; 

public class OkxClientService : AbstractClientService {
    private const string OkxBaseUrl = "https://okx.com";

    public async Task<OkxAnnouncementResponse> CheckOkxAnnouncement() {
        var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var client = new RestClient(OkxBaseUrl);

        var request = new RestRequest($"/v2/support/home/web?t={time}");

        return await client.GetAsync<OkxAnnouncementResponse>(request);
    }
}