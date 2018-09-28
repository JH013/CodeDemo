using Newtonsoft.Json;

namespace Framework.SocketClientLib
{
    public class SocketRequest
    {
        public SocketRequest(string message, string data)
        {
            this.Transcation = RandomString.Generate();
            this.Message = message;
            this.Data = data;
        }

        [JsonProperty("transcation")]
        public string Transcation { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
