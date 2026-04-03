using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Clearance
{
    public class UpdateClearanceStatusDTO
    {
        public int ClearanceId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
