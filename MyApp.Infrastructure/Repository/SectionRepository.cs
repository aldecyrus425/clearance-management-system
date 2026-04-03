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
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public SectionRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Section> addSectionAsync(Section section)
        {
            await _dbContext.Sections.AddAsync(section);
            await _dbContext.SaveChangesAsync();

            return section;
        }

        public async Task<bool> deleteSectionAsync(int id)
        {
            var section = await _dbContext.Sections.FirstOrDefaultAsync(s => s.SectionId == id);
            if(section == null) return false;

            _dbContext.Sections.Remove(section);

            return true;
        }

        public async Task<(IEnumerable<Section>, int totalCounts)> getAllSectionAsync(PaginationDTO dto)
        {
            var query = _dbContext.Sections
                .Include(s => s.Course)
                .Include(s => s.SchoolYear)
                .Include(s => s.YearLevel)
                .AsNoTracking()
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(dto.Search))
            {
                query = query.Where(s =>
                    s.Name.Contains(dto.Search) ||
                    s.Course.Name.Contains(dto.Search) ||
                    s.SchoolYear.Semester.Contains(dto.Search) ||
                    s.SchoolYear.YearStarted.ToString().Contains(dto.Search) ||
                    s.SchoolYear.YearEnd.ToString().Contains(dto.Search) ||
                    s.YearLevel.Name.Contains(dto.Search)
                );
            }

            var totalCounts = await query.CountAsync();

            var section = await query
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (section, totalCounts);
        }

        public async Task<Section?> getSectionByIDAsync(int id)
        {
            return await _dbContext.Sections.FirstOrDefaultAsync(s => s.SectionId == id);
        }

        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
