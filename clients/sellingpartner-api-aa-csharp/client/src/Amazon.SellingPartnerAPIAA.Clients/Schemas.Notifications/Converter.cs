using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.SellingPartnerAPIAA.Clients.Schemas.Notifications
{

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ProcessingStatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ProcessingStatus) || t == typeof(ProcessingStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CANCELLED":
                    return ProcessingStatus.Cancelled;
                case "DONE":
                    return ProcessingStatus.Done;
                case "FATAL":
                    return ProcessingStatus.Fatal;
            }
            throw new Exception("Cannot unmarshal type ProcessingStatus");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ProcessingStatus)untypedValue;
            switch (value)
            {
                case ProcessingStatus.Cancelled:
                    serializer.Serialize(writer, "CANCELLED");
                    return;
                case ProcessingStatus.Done:
                    serializer.Serialize(writer, "DONE");
                    return;
                case ProcessingStatus.Fatal:
                    serializer.Serialize(writer, "FATAL");
                    return;
            }
            throw new Exception("Cannot marshal type ProcessingStatus");
        }

        public static readonly ProcessingStatusConverter Singleton = new ProcessingStatusConverter();
    }


    /// <summary>
    /// An explanation about the purpose of this instance.
    /// </summary>
    public partial class NotificationMetadata
    {
        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>
        [JsonProperty("applicationId")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>
        [JsonProperty("notificationId")]
        public string NotificationId { get; set; }

        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>
        [JsonProperty("publishTime")]
        public string PublishTime { get; set; }

        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>
        [JsonProperty("subscriptionId")]
        public string SubscriptionId { get; set; }
    }

    /// <summary>
    /// An explanation about the purpose of this instance.
    /// </summary>
    public enum ProcessingStatus { Cancelled, Done, Fatal };

}
