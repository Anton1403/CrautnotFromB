using System.Text.Json.Serialization;

namespace Crautnot.Client.Responses; 

public class OkxAnnouncementResponse {
    public int Code { get; set; }
    [JsonPropertyName("data")] public OkxData Data { get; set; }
    public string DetailMsg { get; set; }
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public string Msg { get; set; }
}

public class OkxData {
    public List<Notice> Notices { get; set; }
    public List<object> Banners { get; set; }
}

public class Notice {
    public string Link { get; set; }
    public object PublishDate { get; set; }
    public string ShareLink { get; set; }
    public string ShareText { get; set; }
    public string ShareTitle { get; set; }
    public int Sort { get; set; }
    public object StartTime { get; set; }
    public string Text { get; set; }
    public string Title { get; set; }
}