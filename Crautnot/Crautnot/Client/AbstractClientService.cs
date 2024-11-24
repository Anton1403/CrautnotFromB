using System;

namespace Crautnot.Client
{
    public class AbstractClientService
    {
        protected long ConvertDateToUnxTime(DateTime date, bool toUnixSeconds) {
            var dateTimeStamp = (DateTimeOffset)DateTime.SpecifyKind(date, DateTimeKind.Utc);
            return toUnixSeconds 
                ? dateTimeStamp.ToUnixTimeSeconds() 
                : dateTimeStamp.ToUnixTimeMilliseconds();
        }
    }
}
