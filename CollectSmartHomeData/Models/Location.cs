using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectSmartHomeData.Models {
    public class Location : Room {
        [JsonProperty("room_id")]
        public int RoomId { get; set; }

        [JsonProperty("room_name")]
        public string RoomName { get; set; }

        public override string ToString() {
            return RoomName;
        }

    }
}
