using Crautnot.Client;
using Crautnot.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Crautnot.Controllers
{
    [Route("client")]
    public class ClientController : Controller {
        private readonly MexcClientService _mexcClientService;
        private readonly GateIoClientService _gateIoClientService;

        public ClientController(MexcClientService mexcClientService, GateIoClientService gateIoClientService) {
            _mexcClientService = mexcClientService;
            _gateIoClientService = gateIoClientService;
        }

        #region Mexc

        /// <summary>
        /// Get response of existing token on mexc exchange
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("mexc-token-exist/{token}")]
        public async Task<IActionResult> MexcTokenExist(string token) {
            return Ok(await _mexcClientService.IsTokenExist(token.ToUpper()));
        }

        /// <summary>
        /// Get mexc market data for specific token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("mexc-market-data")]
        public async Task<IActionResult> MexcMarketData([FromBody] GetMarketDataRequest request) {
            return Ok(await _mexcClientService.GetMarketData(request.Token.ToUpper(), request.Start, request.End, request.Interval));
        }

        #endregion

        #region GateIo

        /// <summary>
        /// Get response of existing spot token on GateIo exchange
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gateio-spot-token-exist/{token}")]
        public async Task<IActionResult> GateIoSpotTokenExist(string token) {
            return Ok(await _gateIoClientService.IsSpotTokenExist(token.ToUpper()));
        }

        /// <summary>
        /// Get response of existing futures token on GateIo exchange
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("gateio-futures-token-exist/{token}")]
        public async Task<IActionResult> GateIoFuturesTokenExist(string token) {
            return Ok(await _gateIoClientService.IsFuturesTokenExist(token.ToUpper()));
        }

        /// <summary>
        /// Get mexc market data for specific token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("gateio-market-data")]
        public async Task<IActionResult> GateIoMarketData([FromBody] GetMarketDataRequest request) {
            return Ok(await _gateIoClientService.GetMarketData(request.Token.ToUpper(), request.Start, request.End, request.Interval));
        }

        #endregion
    }
}
