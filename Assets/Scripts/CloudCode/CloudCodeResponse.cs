using Newtonsoft.Json;


namespace CloudCode
{
    public record CloudCodeResponse(string Message)
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; } = Message;
    };
}