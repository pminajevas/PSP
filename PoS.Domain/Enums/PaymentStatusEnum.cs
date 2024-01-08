using System.Text.Json.Serialization;

namespace PoS.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentStatusEnum
    {
        Paid = 0,
        Unpaid = 1,
        Processing = 2
    }
}
