using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elena.Application.DTOs
{
    public class UserLoginDto
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
