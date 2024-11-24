namespace Crautnot.Extensions;

public static class ConvertDate {
    public static DateTime ConvertStringToDateTime(string date) {
        var dateInMilliseconds = long.Parse(date
                                            .Replace("ValueKind = Number", "")
                                            .Replace("\"", ""));
        return DateTimeOffset.FromUnixTimeMilliseconds(dateInMilliseconds).DateTime;
    }
}