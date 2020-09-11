using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectSmartHomeData.Models {
    public class SensorType {
        /// <summary>
        /// ID типа датчика
        /// </summary>
        [JsonProperty("type_id")]
        public int Id { get; set; }

        /// <summary>
        /// Название тип датчика
        /// </summary>
        [JsonProperty("type_name")]
        public string TypeName { get; set; }

        /// <summary>
        /// Факт того, относится ли тип датчика к управляющим устройствам
        /// </summary>
        [JsonProperty("is_actuator")]
        public int IsActuator { get; set; }

        public override string ToString() {
            return TypeName;
        }

    }
}
