using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class Students
    {
        public int StudentsId { get; private set; }
        public int UserId { get; private set; }
        public Users Users { get; private set; }
        public string StudentNumber { get; private set; }
        public string Course {  get; private set; }
        public string YearLevel { get; private set; }
        public string Section {  get; private set; }
        public string Status { get; private set; } // Active, Graduating, Dropped, etc.
        public DateTime CreatedAt { get; private set; }

    }
}
