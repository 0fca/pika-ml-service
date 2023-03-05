using Newtonsoft.Json;


namespace PikaMLModule.Models.Dto
{
    public class InputDto
    {
        [JsonProperty(propertyName:"text")]
        public string Text { get; set; }

        [JsonIgnore]
        public float Irony { get; set; }
    }
}
