using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.Student;
using MyApp.Application.DTO.User;
using MyApp.Application.Interfaces;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class StudentService : IStudentServices
    {
        private readonly IStudentRespository _studentRepo;
        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _transaction;
        public StudentService(IStudentRespository studentRepo, IUserServices userServices, IUnitOfWork transaction)
        {
            _studentRepo = studentRepo;
            _userServices = userServices;
            _transaction = transaction;
        }

        public async Task<ResponseDTO<ShowStudentDTO>> activateStudentAsync(int id)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(id);
                if (response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                response.activateStudent();

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student activated successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> addStudentAsync(CreateStudentDTO dto)
        {
            await _transaction.BeginTransactionAsync();
            try
            {
                var userResponse = await _userServices.addUserAsync(dto.User);

                if (!userResponse.Success)
                {
                    await _transaction.RollbackAsync();
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Adding user error."
                    };
                }

                var student = new Students(userResponse.Data.UserId, dto.StudentNumber, dto.Course, dto.YearLevel, dto.Section);
                var studentResponse = await _studentRepo.addStudentAsync(student);

                await _studentRepo.saveChangesAsync();
                await _transaction.CommitAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student added successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = student.StudentsId,
                        StudentNumber = student.StudentNumber,
                        FullName = student.Users.FirstName + " " + student.Users.LastName,
                        Course = student.Course,
                        YearLevel = student.YearLevel,
                        Section = student.Section
                    }
                };


            }
            catch (ArgumentException ex)
            {
                await _transaction.RollbackAsync();
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> deactivateStudentAsync(int id)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(id);
                if (response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                response.deactivateStudent();

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student deactivated successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> deleteStudentAsync(int id)
        {
            try
            {
                var response = await _studentRepo.deleteStudentAsync(id);
                if (!response)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student deleted successfully.",
                    
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> dropStudentAsync(int id)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(id);
                if (response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                response.droppedStudent();

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student dropped successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<IEnumerable<ShowStudentDTO>>> getAllStudentAsync()
        {
            try
            {
                var response = await _studentRepo.getAllStudentsAsync();

                var students = response.Select(s => new ShowStudentDTO
                {
                    StudentId = s.StudentsId,
                    StudentNumber = s.StudentNumber,
                    FullName = s.Users.FirstName + " " + s.Users.LastName,
                    Course = s.Course,
                    YearLevel = s.YearLevel,
                    Section = s.Section
                });

                return new ResponseDTO<IEnumerable<ShowStudentDTO>>
                {
                    Success = true,
                    Message = "Student Lists",
                    Data = students
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ShowStudentDTO>>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> getStudentByIDAsync(int id)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(id);
                if(response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student information.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> graduateStudentAsync(int id)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(id);
                if (response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                response.droppedStudent();

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student changed status to graduate successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }

        public async Task<ResponseDTO<ShowStudentDTO>> updateStudentAsync(UpdateStudentDTO dto)
        {
            try
            {
                var response = await _studentRepo.getStudentsByIDAsync(dto.StudentID);
                if (response == null)
                {
                    return new ResponseDTO<ShowStudentDTO>
                    {
                        Success = false,
                        Message = "Student not found."
                    };
                }

                response.UpdateStudents(dto.StudentNumber, dto.Course, dto.YearLevel, dto.Section);
                response.Users.UpdateUserNameAndEmail(dto.FirstName, dto.MiddleName, dto.LastName, dto.Email);

                await _studentRepo.saveChangesAsync();

                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = true,
                    Message = "Student updated successfully.",
                    Data = new ShowStudentDTO
                    {
                        StudentId = response.StudentsId,
                        StudentNumber = response.StudentNumber,
                        FullName = response.Users.FirstName + " " + response.Users.LastName,
                        Course = response.Course,
                        YearLevel = response.YearLevel,
                        Section = response.Section
                    }
                };
            }
            catch (ArgumentException ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowStudentDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };

            }
        }
    }
}
