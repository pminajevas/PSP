using System.Text.Json.Serialization;

namespace PoS.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CouponValidityEnum
    {
        True = 0,
        False = 1
    }
}
