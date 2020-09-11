using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Refit;
using System.Threading.Tasks;
using CollectSmartHomeData.Models;

namespace CollectSmartHomeData.Services {
    public class SensorsService {
        private readonly HttpClient _httpClient;
        private readonly ISensorsAPI _sensorsApi;

        public SensorsService(Uri baseUrl) {
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = baseUrl };
            _sensorsApi = RestService.For<ISensorsAPI>(_httpClient);
        }

        /// <summary>
        /// Obtaining list of sensors
        /// </summary>
        /// <returns>Completed task for get sensors</returns>
        public async Task<List<Sensor>> getSensors() {
            return await _sensorsApi.GetSensors().ConfigureAwait(false);
        }

        /// <summary>
        /// Obtaining list of rooms
        /// </summary>
        /// <returns>Completed task for get rooms</returns>
        public async Task<List<Room>> getRooms() {
            return await _sensorsApi.GetRooms().ConfigureAwait(false);
        }

        /// <summary>
        /// Obtaining list of types of sensors
        /// </summary>
        /// <returns>Completed task for get rooms</returns>
        public async Task<List<SensorType>> getSensorTypes() {
            return await _sensorsApi.GetSensorTypes().ConfigureAwait(false);
        }

    }
}
