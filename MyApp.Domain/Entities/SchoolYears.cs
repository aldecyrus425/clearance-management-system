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
        public string Semester { get; private set; } //1st, 2nd, Summer
        public bool IsActive { get; private set; }

        protected SchoolYears() { }

        public SchoolYears(DateOnly yearStarted, DateOnly yearEnd, string semester, bool isActive = false)
        {
            YearStarted = yearStarted;
            YearEnd = yearEnd;
            Semester = semester;
            IsActive = isActive;
        }

        public void Update(DateOnly yearStart, DateOnly yearEnd, string semester)
        {
            YearStarted = yearStart;
            YearEnd = yearEnd;
            Semester = semester;
        }

        public void SetActive()
        {
            IsActive = true;
        }

        public void SetInactive()
        {
            IsActive = false;
        }
    }
}