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
    public class YearLevelRepository : IYearLevelRepository
    {

        private readonly ApplicationDBContext _dbContext;

        public YearLevelRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<YearLevel> addYearLevelAsync(YearLevel yearLevel)
        {
            await _dbContext.YearLevels.AddAsync(yearLevel);
            await _dbContext.SaveChangesAsync();

            return yearLevel;
        }

        public async Task<bool> deleteYearLevelAsync(int id)
        {
            var yearLevel = await _dbContext.YearLevels.FirstOrDefaultAsync(y => y.YearLevelId == id);
            if(yearLevel == null) return false;

            _dbContext.YearLevels.Remove(yearLevel);

            return true;
        }

        public async Task<(IEnumerable<YearLevel>, int totalCounts)> getAllYearLevelAsync(PaginationDTO dto)
        {
            var query = _dbContext.YearLevels.AsNoTracking().AsQueryable();
            if(!string.IsNullOrEmpty(dto.Search))
            {
                query = query.Where(y => 
                y.Name.Contains(dto.Search));
            }

            var totalCount = await query.CountAsync();

            var yearLevels = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (yearLevels, totalCount);
        }

        public async Task<YearLevel?> getYearLevelByIDAsync(int id)
        {
            return await _dbContext.YearLevels.FirstOrDefaultAsync(y => y.YearLevelId == id);
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
