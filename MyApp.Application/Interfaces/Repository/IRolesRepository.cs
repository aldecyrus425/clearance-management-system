using MyApp.Application.DTO.Pagination;
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
        Task<(IEnumerable<Roles>, int totalCounts)> getAllRolesAsync(PaginationDTO dto);
        Task<Roles?> getRolesByIDAsync(int id);
        Task<Roles> addRoleAsync(Roles role);
        Task<bool> deleteRoleAsync(int id);
        Task saveChangesAsync();
    }
}
