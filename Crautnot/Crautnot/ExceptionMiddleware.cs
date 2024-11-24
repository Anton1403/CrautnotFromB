using System;
using System.Threading.Tasks;
using Bugsnag.Payload;
using Crautnot.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Exception = System.Exception;

namespace Crautnot
{
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ErrorNotifier _notifier;

        public ExceptionMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, ErrorNotifier notifier) {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
            _notifier = notifier;
        }

        public async Task Invoke(HttpContext context) {
            try {
                await _next.Invoke(context);
            }
            catch (Exception e) {
                _notifier.Notify(e);
            }
        }
    }
}
