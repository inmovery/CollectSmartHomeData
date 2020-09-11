using CollectSmartHomeData.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectSmartHomeData.Services {
    public interface ISensorsAPI {

        // Получение информации обо всех датчиках
        [Get("/api/sensors")]
        Task<List<Sensor>> GetSensors();

        // Получение информации об одном датчике по ID
        [Get("/api/sensors/{id}")]
        Task<Sensor> GetSensorById(int id);

        // Отправка форма по созданию датчика
        [Multipart]
        [Post("/api/sensors/create")]
        Task UploadPhoto([AliasAs("sensor_name")] string name, [AliasAs("topic")] string topic,
                         [AliasAs("room_id")] int room_id, [AliasAs("room_name")] string room_name,
                         [AliasAs("sensor_type")] string sensor_type, [AliasAs("frequency")] int frequency,
                         [AliasAs("sensor_description")] string description, [AliasAs("is_actuator")] int is_actuator,
                         [AliasAs("image_file")] StreamPart image_stream, [AliasAs("csv_file")] StreamPart csv_stream);

        // Получение информации обо всех типах датчиков
        [Get("/api/sensors/types")]
        Task<List<SensorType>> GetSensorTypes();

        // Отправка формы по созданию типа датчика
        [Post("/api/sensors/types/create")]
        Task CreateSensorType([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        // Получение информации обо всех комнатах
        [Get("/api/rooms")]
        Task<List<Room>> GetRooms();

        // Получение информации обо всех типах датчиков
        [Get("/api/rooms/types")]
        Task<List<RoomType>> GetRoomTypes();

        // Отправка формы по созданию комнаты
        [Post("/api/rooms/create")]
        Task CreateRoom([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

        // Отправка формы по созданию нового типа комнаты
        [Post("/api/rooms/types/create")]
        Task CreateRoomType([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

    }
}
