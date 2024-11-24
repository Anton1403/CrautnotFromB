using Microsoft.AspNetCore.SignalR;

namespace Crautnot.SignalR; 

public class QuartzHub : Hub {
    public async Task SendLastRunTime(string time) {
        await Clients.All.SendAsync("ReceiveLastRunTime", time);
    }
}