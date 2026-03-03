using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<Users>> getAllUserAsync();
        public Task<Users?> getUserByIDAsync(int id);
        public Task<Users> addUserAsync(Users user);
        public Task<bool> deleteUserAsync(int id);
        public Task saveChangesAsync();
    }
}
