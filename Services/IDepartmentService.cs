using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IDepartmentService
    {
        void CreateDepartment(DepartmentDTO departmentDto);
        IEnumerable<DepDTO> GetAllDepartments();
        DepDTO GetDepartmentByid(int departmentId);
        Department GetDepartmentByName(string department);
        string GetDepartmentName(int depId);
        void SetDepartmentActiveStatus(int departmentId, bool isActive);
        void UpdateDepartment(DepDTO departmentDto);
    }
}