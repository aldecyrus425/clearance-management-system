using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Clearance
{
    public class ShowClearanceListDTO
    {
        public int ClearanceId { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string SchoolYear { get; set; }
        public string OverallStatus { get; set; }
    }
}
