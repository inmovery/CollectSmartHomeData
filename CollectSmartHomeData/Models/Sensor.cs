using DevExpress.Mvvm.Native;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xaml.Schema;

namespace CollectSmartHomeData.Models {
    public class Sensor {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("location")]
        public Room Room { get; set; }

        [JsonIgnore]
        public int RoomIndex {
            get {
                return Room.RoomId - 1;
            }
            set {}
        }

        [JsonIgnore]
        public int SensorTypeIndex {
            get {
                return SensorType.Id - 1;
            }
            set { }
        }

        [JsonProperty("sensor_type")]
        public SensorType SensorType { get; set; }

        [JsonProperty("frequency")]
        public int Frequency { get; set; }

        private string mFrequency;

        [JsonIgnore]
        public string FrequencyFull {
            get {
                mFrequency = Frequency.ToString() + " " + getNoun(Frequency, "секунда", "секунды", "секунд");
                return mFrequency;
            }
            set {
                mFrequency = Frequency.ToString() + " " + getNoun(Frequency, "секунда", "секунды", "секунд");
            }
        } 

        [JsonIgnore]
        public string IsActuatorFull {
            get {
                return SensorType.IsActuator == 1 ? "Да" : "Нет";
            }
            set {}
        }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_active")]
        public int IsActive { get; set; }

        [JsonProperty("url_image")]
        public string UrlImage { get; set; }

        [JsonProperty("url_csv")]
        public string UrlCsv { get; set; }

        #region Utils methods

        /// <summary>
        /// The function for calculate ending of the noun related to number
        /// </summary>
        /// <param name="number">Initial number</param>
        /// <param name="one">First form of name related to number</param>
        /// <param name="two">Second form of name related to number</param>
        /// <param name="five">Third form of name related to number</param>
        /// <returns>Formatted string</returns>
        public string getNoun(int number, string one, string two, string five) {
            int n = Math.Abs(number);
            n %= 100;
            if(n >= 5 && n <= 20)
                return five;

            n %= 10;
            if(n == 1)
                return one;

            if(n >= 2 && n <= 4)
                return two;

            return five;

        }

        #endregion Utils methods

    }
}
