using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.Utilities
{
    public class Filter
    {
        private readonly Dictionary<string, object?> _parameters = new Dictionary<string, object?>();

        private static readonly HashSet<string> SupportedParameters = new HashSet<string>
    {
        "Location",
        "OrderBy",
        "Sorting",
        "PageIndex",
        "PageSize",
        "ReservationTimeFrom",
        "ReservationTimeUntil",
        "EmployeeId",
        "RoleName",
        "RoleId",
        "FirstName",
        "LastName",
        "PhoneNumber",
        "Email",
        "Address",
        "HireDate",
        "DiscountId",
        "DiscountId",
        "BusinessId",
        "ValidUntil",
        "Validity",
        // add more :)
    };

        public IReadOnlyDictionary<string, object?> Parameters => _parameters;

        public bool AddParameter(string name, object? value)
        {
            if(value == null)
                return false;

            if (SupportedParameters.Contains(name))
            {
                _parameters[name] = value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Contains(string filterName)
        {
            return _parameters.ContainsKey(filterName);
        }
    }
}
