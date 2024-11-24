using Crautnot.Extensions;
using System.Text.RegularExpressions;

namespace Crautnot.Services; 

public abstract class AbstractService {
    private const string PatternOneWord = @"(\w+)USDT";
    private const string PatternSlash = @"(\w+)/USDT";

    protected string GetTokenName(string symbol) {
        if (symbol.Contains("USDT")) {
            if (symbol.Contains('/')) return Regex.Match(symbol, PatternSlash).Groups[1].Value;

            return Regex.Match(symbol, PatternOneWord).Groups[1].Value;
        }

        return GetTextInParentheses(symbol);
    }

    private string GetTextInParentheses(string title) {
        var startIndex = title.IndexOf('(');
        var endIndex = title.IndexOf(')');

        if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
            return title.Substring(startIndex + 1, endIndex - startIndex - 1);
        return string.Empty;
    }

    protected int GetLeverageOfLiquidationLong(decimal entryPrice, decimal minPrice) {
        decimal maintenanceMargin = 0.005m;
        decimal leverage = (entryPrice * (1 - maintenanceMargin)) / (entryPrice - minPrice);

        int leverageInt = (int)Math.Floor(leverage);

        return leverageInt;
    }

    protected int GetLeverageOfLiquidationShort(decimal entryPrice, decimal maxPrice) {
        decimal maintenanceMargin = 0.005m;
        decimal leverage = (entryPrice * (1 + maintenanceMargin)) / (maxPrice - entryPrice);

        int leverageInt = (int)Math.Floor(leverage);

        return leverageInt;
    }

    protected DateTime ConvertObjectToDateTime(object publishObject) {
        return ConvertDate.ConvertStringToDateTime(publishObject.ToString());
    }
}