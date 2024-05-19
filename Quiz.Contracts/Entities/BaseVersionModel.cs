using System.Text.Json.Serialization;

namespace Quiz.Contracts.Entities
{
    public class BaseVersionModel
    {
        [JsonIgnore]
        public string CreateBy { get; set; } = null!;
        [JsonIgnore]
        public string UpdateBy { get; set; } = null!;
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [JsonIgnore]
        public DateTime UpdateDate { get; set; }

    }
}
