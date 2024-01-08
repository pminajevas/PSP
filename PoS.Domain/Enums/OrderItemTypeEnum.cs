using System.Text.Json.Serialization;

namespace PoS.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderItemTypeEnum
    {
        Item = 0,
        Service = 1,
        Appointment = 2
    }
}
