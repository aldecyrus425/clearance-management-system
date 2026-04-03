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
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        public int YearLevelId { get; private set; }
        public YearLevel YearLevel { get; private set; }
        public int SectionId { get; private set; }
        public Section Section { get; private set; }
        public string Status { get; private set; } // Active, Graduating, Dropped, etc.
        public DateTime CreatedAt { get; private set; }

        protected Students() { }

        public void activateStudent()
        {
            Status = "Active";
        }

        public void deactivateStudent()
        {
            Status = "Deactivate";
        }

        public void droppedStudent()
        {
            Status = "Dropped";
        }

        public void graduateStudent()
        {
            Status = "Graduate";
        }

        public Students(int userID, string studentNumber, int course, int yearLevel, int section)
        {
            if (userID <= 0)
                throw new ArgumentException("User ID must be greater than 0.");

            if (string.IsNullOrWhiteSpace(studentNumber))
                throw new ArgumentException("Student number is required.");

            if (course <= 0)
                throw new ArgumentException("Course ID is invalid.");

            if (yearLevel <= 0)
                throw new ArgumentException("Year level is invalid.");

            if (section <= 0)
                throw new ArgumentException("Section is invalid.");

            UserId = userID;
            StudentNumber = studentNumber.Trim();
            CourseId = course;
            YearLevelId = yearLevel;
            SectionId = section;
            Status = "Deactivated";
            CreatedAt = DateTime.Now;
        }

        public void UpdateStudents(string studentNumber, string course, string yearLevel, string section)
        {

            if (string.IsNullOrWhiteSpace(studentNumber))
                throw new ArgumentException("Student number is required.");

            if (string.IsNullOrWhiteSpace(course))
                throw new ArgumentException("Course is required.");

            if (string.IsNullOrWhiteSpace(yearLevel))
                throw new ArgumentException("Year level is required.");

            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("Section is required.");

            StudentNumber = studentNumber.Trim();
            Course = course.Trim();
            YearLevel = yearLevel.Trim();
            Section = section.Trim();
        }

    }

   

}
