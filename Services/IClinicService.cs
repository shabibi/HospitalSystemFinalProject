using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IClinicService
    {
        void AddClinic(ClinicInput input);
        IEnumerable<Clinic> GetAllClinic();
        IEnumerable<Clinic> GetClinicByBranchDep(int bid, int depid);
        Clinic GetClinicById(int clinicId);
        Clinic GetClinicByName(string clinicName);
        string GetClinicName(int cid);
        IEnumerable<Clinic> GetClinicsByBranchName(string branchName);
        IEnumerable<Clinic> GetClinicsByDepartmentId(int departmentId);
        decimal GetPrice(int clinicId);
        void SetClinicStatus(int clinicId);
        void UpdateClinicDetails(int CID, ClinicInput input);
    }
}