using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectSmartHomeData.Models {
    public class Room {
        /// <summary>
        /// ID комнаты
        /// </summary>
        [JsonProperty("room_id")]
        public int RoomId { get; set; }
        
        /// <summary>
        /// Название комнаты
        /// </summary>
        [JsonProperty("type_name")]
        public string TypeName { get; set; }

        /// <summary>
        /// Длина комнат
        /// </summary>
        [JsonProperty("length")]
        public double Length { get; set; }

        /// <summary>
        /// Ширина комнаты
        /// </summary>
        [JsonProperty("width")]
        public double Width { get; set; }

        /// <summary>
        /// Высота комнаты
        /// </summary>
        [JsonProperty("height")]
        public double Height { get; set; }

        /// <summary>
        /// ID дома, в котором находится комната
        /// </summary>
        [JsonProperty("home_id")]
        public int HomeId { get; set; }

        public override string ToString() {
            return TypeName + " l = " + Length.ToString() + ", w = " + Width.ToString() + ", h = " + Height.ToString();
        }

    }
}
