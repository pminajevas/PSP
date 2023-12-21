using PoS.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.InnerDTOs
{
    public class UserInner
    {

        public string LoginName { get; set; }

        public Guid? UserId { get; set; }

        public Guid? BusinessId { get; set; }
        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public string? JwtToken { get; set; }

        public string? RoleName { get; set; }

    }
}


