using System.Text.Json.Serialization;

namespace PoS.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaxCategoryEnum
    {
        Percent = 0
    }
}
