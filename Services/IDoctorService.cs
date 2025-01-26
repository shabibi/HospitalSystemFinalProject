using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IDoctorService
    {
        void AddDoctor(DoctorInput input);
        bool EmailExists(string email);
        IEnumerable<Doctor> GetAllDoctors();
        IEnumerable<Doctor> GetDoctorByBrancDep(int bid, int depid);
        Doctor GetDoctorByEmail(string email);
        Doctor GetDoctorById(int uid);
        Doctor GetDoctorByName(string docName);
        DoctorOutPutDTO GetDoctorData(string? docName, int? Did);
        IEnumerable<DoctorOutPutDTO> GetDoctorsByBranchName(string branchName);
        IEnumerable<DoctorOutPutDTO> GetDoctorsByDepartmentName(string departmentName);
        void UpdateDoctor(Doctor doctor);
        void UpdateDoctorDetails(DoctorUpdateDTO input);
        DoctorOutPutDTO GetDoctorDetailsById(int uid);

    }
}