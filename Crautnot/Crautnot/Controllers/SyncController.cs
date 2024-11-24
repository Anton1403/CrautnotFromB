using Crautnot.Services;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace Crautnot.Controllers;

public class SyncController : Controller {
    private readonly SynchronizeService synchronizeService;

    public SyncController(SynchronizeService synchronizeService) {
        this.synchronizeService = synchronizeService;
    }

    [HttpPost]
    [Route("synchronize")]
    public async Task<IActionResult> Synchronize(int limit) {
        await synchronizeService.Synchronize(limit, null);
        return Ok();
    }
}