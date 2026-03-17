using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.DTO.Office
{
    public class UpdateOfficeDTO
    {
        public int OfficeID { get; set; }
        public string OfficeName { get; set; }
        public string Description { get; set; }
    }
}
