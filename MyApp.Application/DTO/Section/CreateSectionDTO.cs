using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Section
{
    public class CreateSectionDTO
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public int SchoolYearId { get; set; }
        public int YearLevelId { get; set; }
        public bool IsActive { get; set; }
    }
}
