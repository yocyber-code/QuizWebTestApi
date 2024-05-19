using System.Text.Json.Serialization;

namespace Quiz.Contracts.Entities
{
    public class BaseVersionModel
    {
        [JsonIgnore]
        public DateTime CREATE_DATE { get; set; }
        [JsonIgnore]
        public DateTime UPDATE_DATE { get; set; }
    }
}
