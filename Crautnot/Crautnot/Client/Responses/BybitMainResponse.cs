using System.Text.Json.Serialization;

namespace Crautnot.Client.Responses; 

public class BybitMainResponse {
    public class List {
        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("type")] public Type Type { get; set; }

        [JsonPropertyName("tags")] public List<string> Tags { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; }

        [JsonPropertyName("dateTimestamp")] public object DateTimestamp { get; set; }

        [JsonPropertyName("startDateTimestamp")]
        public object StartDateTimestamp { get; set; }

        [JsonPropertyName("endDateTimestamp")] public object EndDateTimestamp { get; set; }
        [JsonPropertyName("publishTime")] public object PublishTimestamp { get; set; }
    }

    public class Result {
        [JsonPropertyName("total")] public int Total { get; set; }

        [JsonPropertyName("list")] public List<List> List { get; set; }
    }

    public class BybitResponse {
        [JsonPropertyName("retCode")] public int RetCode { get; set; }

        [JsonPropertyName("retMsg")] public string RetMsg { get; set; }

        [JsonPropertyName("result")] public Result Result { get; set; }

        [JsonPropertyName("time")] public long Time { get; set; }
    }

    public class Type {
        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("key")] public string Key { get; set; }
    }
}