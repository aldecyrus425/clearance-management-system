using Microsoft.EntityFrameworkCore;
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
    public class SchoolYearRepository : ISchoolYearRepository
    {
        private readonly ApplicationDBContext _context;

        public SchoolYearRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<SchoolYears> addSchoolYearAsync(SchoolYears schoolYear)
        {
            await _context.SchoolYears.AddAsync(schoolYear);
            await _context.SaveChangesAsync();

            return schoolYear;
        }

        public async Task<bool> deleteSchoolYearAsync(int id)
        {
            var schoolYear = await _context.SchoolYears.FirstOrDefaultAsync(s => s.SchoolYearId == id);
            if (schoolYear == null) return false;

            _context.SchoolYears.Remove(schoolYear);

            return true;
        }

        public async Task<IEnumerable<SchoolYears>> getAllSchoolYearAsync()
        {
            return await _context.SchoolYears.ToListAsync();
        }

        public async Task<SchoolYears?> getSchoolYearByIDAsync(int id)
        {
            return await _context.SchoolYears.FirstOrDefaultAsync(s => s.SchoolYearId == id);
        }

        public async Task saveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
