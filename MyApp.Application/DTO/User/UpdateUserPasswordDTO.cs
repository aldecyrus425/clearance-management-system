using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.User
{
    public class UpdateUserPasswordDTO
    {
        public int UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
    }
}
