using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class AppUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}