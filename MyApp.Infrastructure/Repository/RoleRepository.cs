using Microsoft.EntityFrameworkCore;
using MyApp.Application.DTO.Pagination;
using MyApp.Application.Interfaces.Repository;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyApp.Infrastructure.Repository { 
    public class RoleRespository : IRolesRepository { 

        private readonly ApplicationDBContext _dbContext; 
        public RoleRespository(ApplicationDBContext dbContext) 
        { 
            _dbContext = dbContext; 
        }
        public async Task<Roles> addRoleAsync(Roles role) 
        { 
            await _dbContext.Roles.AddAsync(role); 
            await _dbContext.SaveChangesAsync(); 
            return role; 
        } 
        public async Task<bool> deleteRoleAsync(int id) 
        { 
            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.RoleId == id); 
            if (role == null) return false; 
            _dbContext.Roles.Remove(role); 
            return true; 
        } 
        public async Task<(IEnumerable<Roles>, int totalCounts)> getAllRolesAsync(PaginationDTO dto) 
        { 
            var query = _dbContext.Roles
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(dto.Search))
            {
                query = query.Where(r => 
                r.RoleName.Contains(dto.Search));
            }

            var totalCounts = await query.CountAsync();

            var roles = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (roles, totalCounts);
        } 
        public async Task<Roles?> getRolesByIDAsync(int id) 
        { 
            return await _dbContext.Roles
                .FirstOrDefaultAsync(r => r.RoleId == id); 
        } 
        public async Task saveChangesAsync() 
        { 
            await _dbContext.SaveChangesAsync(); 
        } 
    } 
}