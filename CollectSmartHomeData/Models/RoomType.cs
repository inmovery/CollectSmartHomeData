using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectSmartHomeData.Models {
    public class RoomType {
        /// <summary>
        /// ID типа комнаты
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Название типа комнаты
        /// </summary>
        [JsonProperty("type_name")]
        public string TypeName { get; set; }

    }
}
