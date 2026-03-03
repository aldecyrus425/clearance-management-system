using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class SchoolYears
    {
        public int SchoolYearId { get; private set; }
        public DateOnly YearStarted { get; private set; }
        public DateOnly YearEnd { get; private set; }
        public string Semester {  get; private set; } //1st, 2nd, Summer
        public bool IsActive { get; private set; }
    }
}
