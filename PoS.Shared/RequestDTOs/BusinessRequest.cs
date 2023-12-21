using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Shared.RequestDTOs
{
    public class BusinessRequest
    {

        public string BusinessName { get; set; }

        public string Location { get; set; }

    }
}
