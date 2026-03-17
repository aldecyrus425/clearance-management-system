using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class Offices
    {
        public int OfficeId { get; private set; }
        public string OfficeName { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Offices() { }

        public Offices(string officeName, string description)
        {
            
            if (string.IsNullOrWhiteSpace(officeName))
                throw new ArgumentException("Office name cannot be empty.");

            if (officeName.Length > 100)
                throw new ArgumentException("Office name cannot exceed 100 characters.");

            if (!string.IsNullOrEmpty(description) && description.Length > 255)
                throw new ArgumentException("Description cannot exceed 255 characters.");

            OfficeName = officeName;
            Description = description;
            IsActive = false;
            CreatedAt = DateTime.Now;
        }

        public void UpdateOffice(string officeName, string description)
        {

            if (string.IsNullOrWhiteSpace(officeName))
                throw new ArgumentException("Office name cannot be empty.");

            if (officeName.Length > 100)
                throw new ArgumentException("Office name cannot exceed 100 characters.");

            if (!string.IsNullOrEmpty(description) && description.Length > 255)
                throw new ArgumentException("Description cannot exceed 255 characters.");

            OfficeName = officeName;
            Description = description;
        }

        public void Activate()
        {
            if (IsActive)
                throw new InvalidOperationException("Office is already active.");

            IsActive = true;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Office is already inactive.");

            IsActive = false;
        }
    }


    
}
