using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.SchoolYear
{
    public class ShowSchoolYearDTO
    {
        public int SchoolYearId { get; set; }
        public string SchoolYearDisplay { get; set; } // 2025-2026 1st Semester
        public bool IsActive { get; set; }
    }
}
