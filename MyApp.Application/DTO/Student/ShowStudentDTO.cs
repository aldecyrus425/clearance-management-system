using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Student
{
    public class ShowStudentDTO
    {
        public int StudentId { get; set; }
        public string StudentNumber { get; set; }
        public string FullName { get; set; }
        public string Course { get; set; }
        public string YearLevel { get; set; }
        public string Section { get; set; }
    }
}
