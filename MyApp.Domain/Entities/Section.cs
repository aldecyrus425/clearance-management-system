using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyApp.Domain.Entities
{
    public class Section
    {
        public int SectionId { get; private set; }
        public string Name { get; private set; }
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        public int SchoolYearId { get; private set; }
        public SchoolYears SchoolYear { get; private set; }
        public int YearLevelId { get; private set; }
        public YearLevel YearLevel { get; private set; }
        public bool IsActive { get; private set; }

        protected Section() { }

        public Section(string name, int courseId, int schoolYearId, int yearLevelId, bool isActive)
        {
            if(string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException("Name is required.");

            if (courseId <= 0)
                throw new ArgumentOutOfRangeException("Course ID invalid");

            if (schoolYearId <= 0)
                throw new ArgumentOutOfRangeException("School Year ID invalid");

            if (yearLevelId <= 0)
                throw new ArgumentOutOfRangeException("Year Level ID invalid");

            Name = name;
            CourseId = courseId;
            SchoolYearId = schoolYearId;
            YearLevelId = yearLevelId;
            IsActive = isActive;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Section activated already.");

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Section deactivated already.");

            IsActive = false;
        }

        public void ChangeDetails(string name, int courseId, int schoolYearId, int yearLevelId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name is required.");

            if (courseId <= 0)
                throw new ArgumentOutOfRangeException("Course ID invalid");

            if (schoolYearId <= 0)
                throw new ArgumentOutOfRangeException("School Year ID invalid");

            if (yearLevelId <= 0)
                throw new ArgumentOutOfRangeException("Year Level ID invalid");

            Name = name;
            CourseId = courseId;
            SchoolYearId = schoolYearId;
            YearLevelId = yearLevelId;
        }
    }
}
