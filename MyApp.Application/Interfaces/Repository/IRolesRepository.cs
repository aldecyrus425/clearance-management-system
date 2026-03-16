using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Repository
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> getAllRolesAsync();
        Task<Roles?> getRolesByIDAsync(int id);
        Task<Roles> addRoleAsync(Roles role);
        Task<bool> deleteRoleAsync(int id);
        Task saveChangesAsync();
    }
}
