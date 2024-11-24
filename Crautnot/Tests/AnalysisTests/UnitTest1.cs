using Analytics;
using Newtonsoft.Json;

namespace AnalysisTests
{
    public class UnitTest1
    {
        private string GetProjectRoot()
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.GetFiles(directory).Any(file => file.EndsWith(".csproj")))
            {
                directory = Directory.GetParent(directory).FullName;
            }
            return directory;
        }

        private List<(DateTime Date, double Value)> ReadJsonData(string fileName, Func<PriceData, double> selector)
        {
            var projectRoot = GetProjectRoot();
            var filePath = Path.Combine(projectRoot, fileName);
            var json = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<List<PriceData>>(json);
            return data.Select(d => (d.DateTime, selector(d))).ToList();
        }

       
        [Fact]
        public void AnalyzeTrend_ShouldDetectDownwardTrend_WithClosePrice_19h()
        {
            var data = ReadJsonData("response_1718289764214.json", d => d.ClosePrice);
            var first = data.First();
            var result = TrendAnalysis.AnalyzeDowntrend(data.Where(x => x.Date < first.Date.AddHours(19)).Select(x => new Analytics.Models.DataPoint() {Date = x.Date, Value = x.Value }).ToList());

            //var correctResult = new DateTime(2023, 05, 30, 04, 45, 00);

            Assert.NotNull(result);
            //Assert.True(result <= correctResult);
        }
        
    }
}