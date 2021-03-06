﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using CollectSmartHomeData.RefitInternalGenerated;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace CollectSmartHomeData.RefitInternalGenerated
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
namespace CollectSmartHomeData.Services
{
    using global::CollectSmartHomeData.Models;
    using global::Refit;
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Collections.ObjectModel;
    using global::System.Linq;
    using global::System.Text;
    using global::System.Threading.Tasks;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedISensorsAPI : ISensorsAPI
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedISensorsAPI(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<List<Sensor>> ISensorsAPI.GetSensors()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetSensors", new Type[] {  });
            return (Task<List<Sensor>>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<Sensor> ISensorsAPI.GetSensorById(int id)
        {
            var arguments = new object[] { id };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetSensorById", new Type[] { typeof(int) });
            return (Task<Sensor>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task ISensorsAPI.UploadPhoto(string name, string topic, int room_id, string room_name, string sensor_type, int frequency, string description, int is_actuator, StreamPart image_stream, StreamPart csv_stream)
        {
            var arguments = new object[] { name, topic, room_id, room_name, sensor_type, frequency, description, is_actuator, image_stream, csv_stream };
            var func = requestBuilder.BuildRestResultFuncForMethod("UploadPhoto", new Type[] { typeof(string), typeof(string), typeof(int), typeof(string), typeof(string), typeof(int), typeof(string), typeof(int), typeof(StreamPart), typeof(StreamPart) });
            return (Task)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<List<SensorType>> ISensorsAPI.GetSensorTypes()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetSensorTypes", new Type[] {  });
            return (Task<List<SensorType>>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task ISensorsAPI.CreateSensorType(Dictionary<string, object> data)
        {
            var arguments = new object[] { data };
            var func = requestBuilder.BuildRestResultFuncForMethod("CreateSensorType", new Type[] { typeof(Dictionary<string, object>) });
            return (Task)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<List<Room>> ISensorsAPI.GetRooms()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetRooms", new Type[] {  });
            return (Task<List<Room>>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<List<RoomType>> ISensorsAPI.GetRoomTypes()
        {
            var arguments = new object[] {  };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetRoomTypes", new Type[] {  });
            return (Task<List<RoomType>>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task ISensorsAPI.CreateRoom(Dictionary<string, object> data)
        {
            var arguments = new object[] { data };
            var func = requestBuilder.BuildRestResultFuncForMethod("CreateRoom", new Type[] { typeof(Dictionary<string, object>) });
            return (Task)func(Client, arguments);
        }

        /// <inheritdoc />
        Task ISensorsAPI.CreateRoomType(Dictionary<string, object> data)
        {
            var arguments = new object[] { data };
            var func = requestBuilder.BuildRestResultFuncForMethod("CreateRoomType", new Type[] { typeof(Dictionary<string, object>) });
            return (Task)func(Client, arguments);
        }
    }
}

#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
