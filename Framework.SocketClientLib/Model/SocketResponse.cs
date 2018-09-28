using Newtonsoft.Json;

namespace Framework.SocketClientLib
{
    public class SocketResponse
    {
        [JsonProperty("transcation")]
        public string Transcation { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public ResponseStatus Status { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public enum ResponseStatus
    {
        Success = 0,
        Failed = 1
    }
}
