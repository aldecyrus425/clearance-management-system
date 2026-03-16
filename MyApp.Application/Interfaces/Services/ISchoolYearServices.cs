using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.SchoolYear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface ISchoolYearServices
    {
        Task<ResponseDTO<IEnumerable<ShowSchoolYearDTO>>> getAllSchoolYearAsync();
        Task<ResponseDTO<IEnumerable<ShowSchoolYearDTO>>> GetSchoolYearsBySemesterAsync(string semester);
        Task<ResponseDTO<ShowSchoolYearDTO>> GetActiveSchoolYearAsync();
        Task<ResponseDTO<ShowSchoolYearDTO>> getSchoolYearByIdAsync(int id);
        Task<ResponseDTO<ShowSchoolYearDTO>> addSchoolYearAsync(CreateSchoolYearDTO dto);
        Task<ResponseDTO<ShowSchoolYearDTO>> updateSchoolYearAsync(UpdateSchoolYearDTO dto);
        Task<ResponseDTO<ShowSchoolYearDTO>> deleteSchoolYearAsync(int id);
        Task<ResponseDTO<ShowSchoolYearDTO>> activateSchoolYearAsync(int id);
        Task<ResponseDTO<ShowSchoolYearDTO>> deactivateSchoolYearAsync(int id);
    }
}
