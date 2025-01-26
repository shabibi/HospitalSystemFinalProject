using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Repositories;

namespace HospitalSystemTeamTask.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public void AddBranch(BranchDTO branchDto)
        {
            var branch = new Branch
            {
                BranchName = branchDto.BranchName.ToLower(),
                Location = branchDto.Location.ToLower(),
                IsActive = branchDto.BranchStatus // Assign from DTO
            };
            _branchRepository.AddBranch(branch); // Save branch to the database
        }
        public void UpdateBranch(int branchId, UpdateBranchDTO branchDto)
        {
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch with ID '{branchId}' not found.");
            }

            // Update fields
            if (!string.IsNullOrWhiteSpace(branchDto.BranchName))
                branch.BranchName = branchDto.BranchName;

            if (!string.IsNullOrWhiteSpace(branchDto.Location))
                branch.Location = branchDto.Location;

            // Save changes
            _branchRepository.UpdateBranch(branch);
        }

        public IEnumerable<Branch> GetAllBranches()
        {
            try
            {
                // Get all branches from the repository
                return _branchRepository.GetAllBranches();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching branches.", ex);
            }
        }

        public Branch GetBranchDetailsByBranchName(string branchName)
        {
            var branch = _branchRepository.GetBranchByBranchName(branchName);

            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch with name '{branchName}' not found.");
            }

            // Return BranchDTO instead of the Branch model
            return new Branch
            {
                BranchName = branch.BranchName,
                Location = branch.Location,
                BID = branch.BID,
                IsActive = branch.IsActive
            };
        }

        public string GetBranchName(int branchId)
        {
            return _branchRepository.GetBranchName(branchId);
        }
        public void SetBranchStatus(int branchId, bool isActive)
        {
            // Retrieve the branch by name
            var branch = _branchRepository.GetBranchById(branchId);
            if (branch == null)
            {
                throw new KeyNotFoundException($"Branch with ID ' {branchId}' not found.");
            }

            // Update the IsActive flag
            branch.IsActive = isActive;

            // Save the updated branch
            _branchRepository.UpdateBranch(branch);
        }
        public BranchDTO GetBranchById(int id)
        {
            var branch = _branchRepository.GetBranchById(id);
            if (branch == null)
            {
                return null; // or throw an exception, depending on your design
            }

            // Map Branch to BranchDTO
            return new BranchDTO
            {
                BranchName = branch.BranchName,
                Location = branch.Location,
                BranchStatus = branch.IsActive
            };
        }

        public BranchDTO GetBranchDetails(string? branchName, int? branchId)
        {
            Branch branch = null;

            // Validate that at least one parameter is provided
            if (string.IsNullOrWhiteSpace(branchName) && !branchId.HasValue)
                throw new ArgumentException("Either branch name or branch ID must be provided.");

            // Retrieve branch based on branch name
            if (!string.IsNullOrEmpty(branchName))
                branch = _branchRepository.GetBranchByBranchName(branchName);

            // Retrieve branch based on branch ID
            if (branchId.HasValue)
                branch = _branchRepository.GetBranchById(branchId.Value);

            if (branch == null)
                throw new KeyNotFoundException("Branch not found.");

            // Map to BranchDTO
            var branchDTO = new BranchDTO
            {
                BranchName = branch.BranchName,
                Location = branch.Location,
                BranchStatus = branch.IsActive
            };

            return branchDTO;
        }


    }
}
