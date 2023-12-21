using PoS.Shared.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Shared.ResponseDTOs
{
    public class RoleResponse
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string RoleName { get; set; }
    }
}
