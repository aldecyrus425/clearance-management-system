using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.YearLevel
{
    public class UpdateYearLevelDTO
    {
        public int YearLevelId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
