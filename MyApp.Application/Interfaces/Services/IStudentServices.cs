using MyApp.Application.DTO.Pagination;
using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Interfaces.Services
{
    public interface IStudentServices
    {
        public Task<ResponseDTO<IEnumerable<ShowStudentDTO>>> getAllStudentAsync(PaginationDTO dto);
        public Task<ResponseDTO<ShowStudentDTO>> getStudentByIDAsync(int id);
        public Task<ResponseDTO<ShowStudentDTO>> addStudentAsync(CreateStudentDTO dto);
        public Task<ResponseDTO<ShowStudentDTO>> updateStudentAsync(UpdateStudentDTO dto);
        public Task<ResponseDTO<ShowStudentDTO>> activateStudentAsync(int id);
        public Task<ResponseDTO<ShowStudentDTO>> deactivateStudentAsync(int id);
        public Task<ResponseDTO<ShowStudentDTO>> dropStudentAsync(int id);
        public Task<ResponseDTO<ShowStudentDTO>> graduateStudentAsync(int id);
        public Task<ResponseDTO<ShowStudentDTO>> deleteStudentAsync(int id);

    }
}
