using Newtonsoft.Json;

namespace QuestionApp.Api.Models;

public class Question
{
    [JsonProperty(PropertyName = "id")]
    public string Id {get; set;} = string.Empty;
    
    [JsonProperty(PropertyName = "text")]
    public string Text { get; set; } = string.Empty;
    public bool Required { get; set; }

    public QuestionType Type { get; set; }

    public List<string> Options { get; set; }
}