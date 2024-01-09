using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PoS.Application.Filters
{
    public class BaseFilter
    {
        public int Page { get; set; } = 1;

        [Range(1, DefaultPaginationParameters.MaximumPageSize)]
        public int PageSize { get; set; } = DefaultPaginationParameters.MaximumPageSize;

        public string OrderBy { get; set; } = string.Empty;

        public Sorting? Sorting { get; set; } = null;

        public int ItemsToSkip()
        {
            return (Page - 1) * PageSize;
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sorting
    {
        asc,
        dsc
    }
}
