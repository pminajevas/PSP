using PoS.Data;
using PoS.Shared.Utilities;
using System.Net;
using System.Text.RegularExpressions;

namespace PoS.API.Helpers
{
    public class FilterValidator : IFilterValidator
    {

        public bool ValidateFilter(Filter filter)
        {
            foreach (var filterName in filter.Parameters.Keys)
            {
                if (filterName == "OrderBy")
                {
                    return true;
                    // checking field name
                }
                else if (filterName == "Sorting")
                {
                    return Regex.IsMatch((string)filter.Parameters[filterName], "^(asc|desc)$", RegexOptions.IgnoreCase);
                }
                else if (filterName == "PageIndex")
                {

                    return (int)filter.Parameters[filterName] >= 0;
                }
                else if (filterName == "PageSize")
                {

                    return (int)filter.Parameters[filterName] > 0;
                }
                else if (filterName == "email")
                {
                    string emailPattern = @"^\S+@\S+\.\S+$";
                    return Regex.IsMatch((string)filter.Parameters[filterName], emailPattern);
                }
                else if(filter.Parameters[filterName].GetType() == typeof(string))
                {
                    return !string.IsNullOrEmpty((string)filter.Parameters[filterName]);
                }
            }


            return true;
        }
    }
}
