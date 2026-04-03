using MyApp.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Student
{
    public class CreateStudentDTO
    {
        public CreateUserDTO User { get; set; }
        public string StudentNumber { get; set; }
        public int Course { get; set; }
        public int YearLevel { get; set; }
        public int Section { get; set; }
    }
}
