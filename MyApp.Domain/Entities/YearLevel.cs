using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class YearLevel
    {
        public int YearLevelId { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        protected YearLevel() { }

        public YearLevel(string name, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            Name = name;
            IsActive = isActive;
        }

        public void RenameYearLevel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            Name = name;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Year level activated already.");

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Year level is deativated already.");

            IsActive = false;
        }
    }
}
