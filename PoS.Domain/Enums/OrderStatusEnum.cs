using System.Text.Json.Serialization;

namespace PoS.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatusEnum
    {
        Paid = 0,
        Unpaid = 1
    }
}
