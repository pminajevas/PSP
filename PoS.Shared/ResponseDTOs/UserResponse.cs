using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PoS.Shared.Utilities;

namespace PoS.Shared.ResponseDTOs
{
    public class UserResponse
    {
        public Guid? Id { get; set; }

        public string LoginName { get; set; }

        public string JwtToken { get; set; }

        public string RoleName {  get; set; }
    }
}
