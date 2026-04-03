using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Clearance
{
    public class ClearanceStatusDTO
    {
        public int ClearanceStatusId { get; set; }
        public int ClearanceId { get; set; }
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }
        public int ClearedBy { get; set; }
        public string? ClearedByName { get; set; }
        public DateTime? ClearedAt { get; set; }
    }
}
