using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class LoginRequestDto
    {   
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}