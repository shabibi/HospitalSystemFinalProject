using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IBranchDepartmentService
    {
        void AddDepartmentToBranch(BranchDepDTO department);
        IEnumerable<DepDTO> GetDepartmentsByBranch(int bid);
        IEnumerable<Branch> GetBranchsByDepartment(int did);
        void UpdateBranchDepartment(BranchDepartment branchDepartment);
        BranchDepartment GetBranchDep(int departmentId, int branchId);
        IEnumerable<DepDTO> GetDepartmentsByBranchName(string branchName);

    }
}