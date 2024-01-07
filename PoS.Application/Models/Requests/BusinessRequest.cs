using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Application.Models.Requests
{
    public class BusinessRequest
    {

        public string BusinessName { get; set; }

        public string Location { get; set; }

    }
}
