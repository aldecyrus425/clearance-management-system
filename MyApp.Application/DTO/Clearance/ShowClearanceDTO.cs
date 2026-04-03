using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Clearance
{

    public class ShowClearanceDTO
    {
        public int ClearanceId { get; set; }
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string SchoolYear { get; set; }
        public string OverallStatus { get; set; }

        public List<ShowClearanceStatusDTO> Offices { get; set; }
    }

    public class ShowClearanceStatusDTO
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }
        public DateTime? ClearedAt { get; set; }
    }
}
