using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class Course
    {
        public int CourseId { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        protected Course() { }

        public Course(string name, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            Name = name;
            IsActive = isActive;
        }


        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            Name = name;
        }

        public void ActivateCourse()
        {
            if (IsActive)
                throw new InvalidOperationException("Course is already activated.");

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Course is already deactivated.");
            IsActive = false;
        }
    }


}
