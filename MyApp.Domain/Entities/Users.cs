using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class Users
    {
        public int UserId { get; private set; }
        public string FirstName { get; private set; }
        public string? MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public int RoleId { get; private set; }
        public Roles Roles { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Users() { }

        public Users(string firstname, string? middlename, string lastname, string email, string hashPassword, int roleId)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentException("First name is required.", nameof(firstname));

            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Last name is required.", nameof(lastname));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Email format is invalid.", nameof(email));

            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new ArgumentException("Password hash is required.", nameof(hashPassword));

            if (roleId <= 0)
                throw new ArgumentException("RoleId must be greater than zero.", nameof(roleId));

            FirstName = firstname;
            MiddleName = middlename;
            LastName = lastname;
            Email = email;
            PasswordHash = hashPassword;
            RoleId = roleId;
        }


        public void ToggleUser()
        {
            IsActive = !IsActive;
        }

        public void UpdateUser(string firstname, string? middlename, string lastname, string email, string hashPassword, int roleId)
        {
            if (string.IsNullOrWhiteSpace(firstname))
                throw new ArgumentException("First name is required.", nameof(firstname));

            if (string.IsNullOrWhiteSpace(lastname))
                throw new ArgumentException("Last name is required.", nameof(lastname));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Email format is invalid.", nameof(email));

            if (string.IsNullOrWhiteSpace(hashPassword))
                throw new ArgumentException("Password hash is required.", nameof(hashPassword));

            if (roleId <= 0)
                throw new ArgumentException("RoleId must be greater than zero.", nameof(roleId));

            FirstName = firstname;
            MiddleName = middlename;
            LastName = lastname;
            Email = email;
            PasswordHash = hashPassword;
            RoleId = roleId;
            IsActive = false;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
