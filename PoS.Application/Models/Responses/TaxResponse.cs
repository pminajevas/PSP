using PoS.Core.Enums;

namespace PoS.Application.Models.Responses
{
    public class TaxResponse
    {
        public Guid? Id { get; set; }

        public string? TaxName { get; set; }

        public string? TaxDescription { get; set; }

        public double? TaxValue { get; set; }

        public TaxCategoryEnum? Category { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }
    }
}
