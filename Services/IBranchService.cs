using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IBranchService
    {
        void AddBranch(BranchDTO branchDto);
        IEnumerable<Branch> GetAllBranches();
        BranchDTO GetBranchById(int id);
        BranchDTO GetBranchDetails(string? branchName, int? branchId);
        Branch GetBranchDetailsByBranchName(string branchName);
        string GetBranchName(int branchId);
        void SetBranchStatus(int branchId, bool isActive);
        void UpdateBranch(int branchId, BranchDTO branchDto);
    }
}