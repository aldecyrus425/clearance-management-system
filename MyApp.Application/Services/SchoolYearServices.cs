using MyApp.Application.DTO.Response;
using MyApp.Application.DTO.SchoolYear;
using MyApp.Application.Interfaces.Repository;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class SchoolYearServices : ISchoolYearServices
    {
        private readonly ISchoolYearRepository _schoolYearRepository;
        private readonly IStudentRespository _studentRepository;
        private readonly IClearancesRespository _clearancesRepository;
        private readonly IClearanceStatusRepository _clearanceStatusRepository;
        private readonly IOfficeRepository _officeRepository;

        public SchoolYearServices(
            ISchoolYearRepository schoolYearRepository, 
            IStudentRespository studentRepository,
            IClearancesRespository clearancesRepository,
            IClearanceStatusRepository clearanceStatusRepository,
            IOfficeRepository officeRepository)
        {
            _schoolYearRepository = schoolYearRepository;
            _studentRepository = studentRepository;
            _clearancesRepository = clearancesRepository;
            _clearanceStatusRepository = clearanceStatusRepository;
            _officeRepository = officeRepository;
        }

        public async Task<ResponseDTO<IEnumerable<ShowSchoolYearDTO>>> getAllSchoolYearAsync()
        {
            var schoolYears = await _schoolYearRepository.getAllSchoolYearAsync();

            var result = schoolYears.Select(s => new ShowSchoolYearDTO
            {
                SchoolYearId = s.SchoolYearId,
                SchoolYearDisplay = $"{s.YearStarted.Year}-{s.YearEnd.Year} {s.Semester}",
                IsActive = s.IsActive
            });

            return new ResponseDTO<IEnumerable<ShowSchoolYearDTO>>
            {
                Success = true,
                Message = "School years retrieved successfully",
                Data = result
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> getSchoolYearByIdAsync(int id)
        {
            var schoolYear = await _schoolYearRepository.getSchoolYearByIDAsync(id);
            if (schoolYear == null)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = "School year not found",
                    Data = null
                };
            }

            var dto = new ShowSchoolYearDTO
            {
                SchoolYearId = schoolYear.SchoolYearId,
                SchoolYearDisplay = $"{schoolYear.YearStarted.Year}-{schoolYear.YearEnd.Year} {schoolYear.Semester}",
                IsActive = schoolYear.IsActive
            };

            return new ResponseDTO<ShowSchoolYearDTO>
            {
                Success = true,
                Message = "School year retrieved successfully",
                Data = dto
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> addSchoolYearAsync(CreateSchoolYearDTO dto)
        {
            var schoolYear = new SchoolYears(
                yearStarted: dto.YearStart,
                yearEnd: dto.YearEnd,
                semester: dto.Semester,
                isActive: false
            );

            var added = await _schoolYearRepository.addSchoolYearAsync(schoolYear);
            await _schoolYearRepository.saveChangesAsync();

            var resultDto = new ShowSchoolYearDTO
            {
                SchoolYearId = added.SchoolYearId,
                SchoolYearDisplay = $"{added.YearStarted.Year}-{added.YearEnd.Year} {added.Semester}",
                IsActive = added.IsActive
            };

            return new ResponseDTO<ShowSchoolYearDTO>
            {
                Success = true,
                Message = "School year added successfully",
                Data = resultDto
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> updateSchoolYearAsync(UpdateSchoolYearDTO dto)
        {
            var existing = await _schoolYearRepository.getSchoolYearByIDAsync(dto.SchoolyearID);
            if (existing == null)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = "School year not found",
                    Data = null
                };
            }

            existing.Update(dto.YearStart, dto.YearEnd, dto.Semester);

            await _schoolYearRepository.saveChangesAsync();

            var resultDto = new ShowSchoolYearDTO
            {
                SchoolYearId = existing.SchoolYearId,
                SchoolYearDisplay = $"{existing.YearStarted.Year}-{existing.YearEnd.Year} {existing.Semester}",
                IsActive = existing.IsActive
            };

            return new ResponseDTO<ShowSchoolYearDTO>
            {
                Success = true,
                Message = "School year updated successfully",
                Data = resultDto
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> deleteSchoolYearAsync(int id)
        {
            var success = await _schoolYearRepository.deleteSchoolYearAsync(id);
            if (!success)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = "School year not found",
                    Data = null
                };
            }

            await _schoolYearRepository.saveChangesAsync();

            return new ResponseDTO<ShowSchoolYearDTO>
            {
                Success = true,
                Message = "School year deleted successfully",
                Data = null
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> GetActiveSchoolYearAsync()
        {
            var allYears = await _schoolYearRepository.getAllSchoolYearAsync();
            var active = allYears.FirstOrDefault(s => s.IsActive);

            if (active == null)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = "No active school year found",
                    Data = null
                };
            }

            return new ResponseDTO<ShowSchoolYearDTO>
            {
                Success = true,
                Message = "Active school year retrieved",
                Data = new ShowSchoolYearDTO
                {
                    SchoolYearId = active.SchoolYearId,
                    SchoolYearDisplay = $"{active.YearStarted.Year}-{active.YearEnd.Year} {active.Semester}",
                    IsActive = active.IsActive
                }
            };
        }

        public async Task<ResponseDTO<IEnumerable<ShowSchoolYearDTO>>> GetSchoolYearsBySemesterAsync(string semester)
        {
            var allYears = await _schoolYearRepository.getAllSchoolYearAsync();
            var filtered = allYears
                .Where(s => string.Equals(s.Semester, semester, StringComparison.OrdinalIgnoreCase))
                .Select(s => new ShowSchoolYearDTO
                {
                    SchoolYearId = s.SchoolYearId,
                    SchoolYearDisplay = $"{s.YearStarted.Year}-{s.YearEnd.Year} {s.Semester}",
                    IsActive = s.IsActive
                });

            return new ResponseDTO<IEnumerable<ShowSchoolYearDTO>>
            {
                Success = true,
                Message = $"School years for {semester} semester retrieved",
                Data = filtered
            };
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> activateSchoolYearAsync(int id)
        {
            try
            {
                var allYears = await _schoolYearRepository.getAllSchoolYearAsync();
                var target = await _schoolYearRepository.getSchoolYearByIDAsync(id);

                if (target == null)
                    return new ResponseDTO<ShowSchoolYearDTO> { Success = false, Message = "School year not found" };

                var activeYear = allYears.FirstOrDefault(s => s.IsActive);
                if (activeYear != null && activeYear.SchoolYearId != id)
                    activeYear.SetInactive();

                target.SetActive();
                await _schoolYearRepository.saveChangesAsync();

                // --- AUTOMATIC CLEARANCE CREATION ---
                var students = await _studentRepository.getAllStudentsAsync();
                var offices = await _officeRepository.getAllOfficesAsync();

                foreach (var student in students)
                {
                    var clearance = new Clearances(student.StudentsId, target.SchoolYearId);
                    await _clearancesRepository.AddClearanceAsync(clearance);

                    // create statuses for all offices
                    var statuses = offices.Select(o => new ClearanceStatuses(clearance.ClearanceId, o.OfficeId));
                    await _clearanceStatusRepository.addRangeAsync(statuses);
                }

                await _clearancesRepository.SaveChangesAsync();
                await _clearanceStatusRepository.saveChangesAsync();

                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = true,
                    Message = "School year activated and clearances created successfully",
                    Data = new ShowSchoolYearDTO
                    {
                        SchoolYearId = target.SchoolYearId,
                        SchoolYearDisplay = $"{target.YearStarted.Year}-{target.YearEnd.Year} {target.Semester}",
                        IsActive = target.IsActive
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseDTO<ShowSchoolYearDTO>> deactivateSchoolYearAsync(int id)
        {
            try
            {
                var target = await _schoolYearRepository.getSchoolYearByIDAsync(id);

                if (target == null)
                {
                    return new ResponseDTO<ShowSchoolYearDTO>
                    {
                        Success = false,
                        Message = "School year not found",
                        Data = null
                    };
                }

                target.SetInactive();
                await _schoolYearRepository.saveChangesAsync();

                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = true,
                    Message = "School year deactivated successfully",
                    Data = new ShowSchoolYearDTO
                    {
                        SchoolYearId = target.SchoolYearId,
                        SchoolYearDisplay = $"{target.YearStarted.Year}-{target.YearEnd.Year} {target.Semester}",
                        IsActive = target.IsActive
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ShowSchoolYearDTO>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}