using Analytics.Models;
using Accord.Statistics.Models.Regression.Linear;

namespace Analytics
{
    public class TrendAnalysis
    {
        public static DataPoint AnalyzeDowntrend(
    List<DataPoint> data,
    double minRecentSegmentHours = 3)
        {
            // Проверяем, что данные не пустые
            if (data == null || data.Count == 0)
            {
                return null;
            }

            // Определяем конец периода анализа (текущая последняя дата в массиве)
            DateTime endDate = data.Max(x => x.Date);

            // Начало анализа - последние minRecentSegmentHours часов до конца
            DateTime startDate = endDate.AddHours(-minRecentSegmentHours);

            // Фильтруем данные за последние minRecentSegmentHours часов
            var recentData = data.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

            // Если данных недостаточно для анализа (менее двух точек), возвращаем null
            if (recentData.Count < 2)
            {
                return null;
            }

            // Преобразуем даты и значения в массивы
            double[] recentDates = recentData.Select(x => x.Date.Subtract(DateTime.MinValue).TotalDays).ToArray();
            double[] recentValues = recentData.Select(x => x.Value).ToArray();

            // Создаем модель линейной регрессии для последних minRecentSegmentHours часов
            SimpleLinearRegression recentRegression = new SimpleLinearRegression();
            recentRegression.Regress(recentDates, recentValues);

            bool recentDowntrend = recentRegression.Slope < 0;

            if (recentDowntrend)
            {
                // Ищем точку начала нисходящей тенденции во всем периоде
                DateTime? downtrendStartDate = FindDowntrendStartDate(data);
                if (downtrendStartDate.HasValue)
                {
                    var downtrendPoint = data.First(x => x.Date == downtrendStartDate.Value);
                    return new DataPoint
                    {
                        Date = downtrendPoint.Date,
                        Value = downtrendPoint.Value
                    };
                }
            }

            // Если наклон не отрицательный или спад временный, возвращаем null
            return null;
        }

        private static DateTime? FindDowntrendStartDate(List<DataPoint> data)
        {
            var previousData = data;

            // Идем с конца и проверяем сегменты
            int i = previousData.Count - 2;
            while (i >= 1)
            {
                // Если наклон становится положительным, выходим из цикла
                if (previousData[i].Value > previousData[i - 1].Value)
                {
                    // Начало нисходящей тенденции - следующая точка после положительного наклона
                    return previousData[i].Date;
                }

                i--;
            }

            // Если цикл завершился и наклон всегда оставался отрицательным, возвращаем самую раннюю дату
            return previousData.First().Date;
        }
    }
}
