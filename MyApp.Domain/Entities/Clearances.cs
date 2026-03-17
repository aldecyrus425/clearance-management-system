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

        protected Clearances() { }

        public Clearances(int studentId, int schoolYearId)
        {
            if (studentId <= 0)
                throw new ArgumentException("Invalid student ID.");

            if (schoolYearId <= 0)
                throw new ArgumentException("Invalid school year ID.");

            StudentId = studentId;
            SchoolYearId = schoolYearId;

            OverallStatus = "Pending";
            CreatedAt = DateTime.Now;
        }

        public void Complete()
        {
            if (OverallStatus == "Completed")
                throw new InvalidOperationException("Clearance already completed.");

            OverallStatus = "Completed";
            CompletedAt = DateTime.Now;
        }

        public void Reset()
        {
            if (OverallStatus == "Completed")
                throw new InvalidOperationException("Cannot reset a completed clearance.");

            OverallStatus = "Pending";
            CompletedAt = null;
        }


    }
}
