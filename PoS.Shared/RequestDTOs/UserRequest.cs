using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PoS.Shared.Utilities;

namespace PoS.Shared.RequestDTOs
{
    public class UserRequest
    {

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string RoleName {  get; set; }

    }
}
