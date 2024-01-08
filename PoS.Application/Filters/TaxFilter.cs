using PoS.Core.Enums;

namespace PoS.Application.Filters
{
    public class TaxFilter : BaseFilter
    {
        public TaxCategoryEnum? Category { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
