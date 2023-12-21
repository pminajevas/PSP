using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Shared.RequestDTOs
{
    public class RoleRequest
    {
        public string RoleName { get; set; }

        public string? Description { get; set; }
    }
}
