using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoS.Services.Filters
{
    public class BaseFilter
    {
        public int Page { get; set; } = 1;

        [Range(1, DefaultPaginationParameters.MaximumPageSize)]
        public int PageSize { get; set; } = DefaultPaginationParameters.MaximumPageSize;

        public string OrderBy { get; set; } = string.Empty;

        public Sorting? Sorting { get; set; }

        public int ItemsToSkip()
        {
            return (this.Page - 1) * this.PageSize;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        asc,
        dsc
    }
}
