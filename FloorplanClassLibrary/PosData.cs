using Newtonsoft.Json;

namespace FloorplanClassLibrary
{
    public class PosData
    {
        [JsonProperty("check_ids")]
        public List<string>? CheckIds { get; set; }

        [JsonProperty("pos_sub_total")]
        public int? PosSubTotal { get; set; }

        [JsonProperty("pos_tax")]
        public int? PosTax { get; set; }

        [JsonProperty("pos_tip")]
        public int? PosTip { get; set; }

        [JsonProperty("pos_total_spend")]
        public int? PosTotalSpend { get; set; }
    }
}