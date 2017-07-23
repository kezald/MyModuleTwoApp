using Newtonsoft.Json;

namespace MyModuleTwoApp
{
   public class TableModel
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
    }
}
