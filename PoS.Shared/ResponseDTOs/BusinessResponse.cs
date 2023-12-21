using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Shared.ResponseDTOs
{
    public class BusinessResponse
    {

        public Guid? Id { get; set; }

        public string BusinessName { get; set; }

        public string Location { get; set; }

    }
}
