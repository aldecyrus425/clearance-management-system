using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class Clearances
    {
        public int ClearanceId { get; private set; }
        public int StudentId { get; private set; }
        public Students Students { get; private set; }
        public int SchoolYearId { get; private set; }
        public SchoolYears SchoolYears { get; private set; }
        public string OverallStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
    }
}
