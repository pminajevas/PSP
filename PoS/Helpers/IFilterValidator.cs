using PoS.Shared.Utilities;

namespace PoS.API.Helpers
{
    public interface IFilterValidator
    {
        bool ValidateFilter(Filter filter);
    }
}