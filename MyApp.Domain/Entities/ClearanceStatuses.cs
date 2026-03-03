using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class ClearanceStatuses
    {
        public int ClearanceStatusId { get; private set; }
        public int ClearanceId { get; private set; }
        public Clearances Clearances { get; private set; }
        public int OfficeId { get; private set; }
        public Offices Offices { get; private set; }
        public string Status {  get; private set; }
        public string Remarks { get; private set; }
        public int ClearedBy { get; private set; }
        public Users Users { get; private set; }
        public DateTime? ClearedAt { get; private set; }

    }
}
