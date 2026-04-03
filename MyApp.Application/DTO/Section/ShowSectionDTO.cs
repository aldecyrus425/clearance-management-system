using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Section
{
    public class ShowSectionDTO
    {
        public int SectionId { get; set; }
        public string Name { get; set; }
        public string CourseName { get; set; }
        public string SchoolYear { get; set; }
        public string YearLevel { get; set; }
        public bool IsActive { get; set; }
    }
}
