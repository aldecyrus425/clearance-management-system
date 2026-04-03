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

namespace MyApp.Infrastructure.Repository
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public OfficeRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Offices> addOfficeAsync(Offices office)
        {
            await _dbContext.Offices.AddAsync(office);
            await _dbContext.SaveChangesAsync();

            return office;
        }

        public async Task<IEnumerable<Offices>> getAllOffices()
        {
            return await _dbContext.Offices.AsNoTracking().ToListAsync();
        }

        public async Task<(IEnumerable<Offices>, int totalCounts)> getAllOfficesAsync(PaginationDTO dto)
        {
            var query = _dbContext.Offices
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrEmpty(dto.Search))
            {
                query = query.Where(o => 
                o.OfficeName.Contains(dto.Search) || 
                o.Description.Contains(dto.Search));
            }

            var totalCounts = await query.CountAsync();

            var offices = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (offices, totalCounts);
        }

        public async Task<Offices?> getOfficeByIDAsync(int id)
        {
            return await _dbContext.Offices.FirstOrDefaultAsync(o => o.OfficeId == id);
        }

        public async Task<bool> removeOfficeAsync(int id)
        {
            var office = await _dbContext.Offices.FirstOrDefaultAsync(o => o.OfficeId == id);
            if (office == null) return false;

            _dbContext.Offices.Remove(office);

            return true;
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
