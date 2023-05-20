using CommonLibrary.LibraryModels;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ModelLibrary.JsonModels
{
    public class DataMessage
    {
        public string Data { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DataType Type { get; set; }

    }
}
