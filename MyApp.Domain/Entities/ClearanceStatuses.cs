using System;

namespace MyApp.Domain.Entities
{
    public class ClearanceStatuses
    {
        public int ClearanceStatusId { get; private set; }

        public int ClearanceId { get; private set; }
        public Clearances Clearances { get; private set; }

        public int OfficeId { get; private set; }
        public Offices Offices { get; private set; }

        public string Status { get; private set; } // Pending, Approved, Rejected

        public string? Remarks { get; private set; }

        public int ClearedBy { get; private set; }
        public Users Users { get; private set; }

        public DateTime? ClearedAt { get; private set; }

        protected ClearanceStatuses() { }

        public ClearanceStatuses(int clearanceId, int officeId)
        {
            if (clearanceId <= 0)
                throw new ArgumentException("Invalid clearance ID.");

            if (officeId <= 0)
                throw new ArgumentException("Invalid office ID.");

            ClearanceId = clearanceId;
            OfficeId = officeId;

            Status = "Pending";
        }

        public void Approve(int userId, string? remarks = null)
        {
            if (Status == "Approved")
                throw new InvalidOperationException("Already approved.");

            if (Status == "Rejected")
                throw new InvalidOperationException("Cannot approve a rejected clearance. Reset first.");

            if (userId <= 0)
                throw new ArgumentException("Invalid user.");

            Status = "Approved";
            ClearedBy = userId;
            Remarks = remarks;
            ClearedAt = DateTime.Now;
        }

        public void Reject(int userId, string remarks)
        {
            if (Status == "Approved")
                throw new InvalidOperationException("Cannot reject an approved clearance.");

            if (string.IsNullOrWhiteSpace(remarks))
                throw new ArgumentException("Remarks are required when rejecting.");

            if (userId <= 0)
                throw new ArgumentException("Invalid user.");

            Status = "Rejected";
            ClearedBy = userId;
            Remarks = remarks;
            ClearedAt = DateTime.Now;
        }

        public void Reset()
        {
            Status = "Pending";
            Remarks = null;
            ClearedBy = 0;
            ClearedAt = null;
        }
    }
}