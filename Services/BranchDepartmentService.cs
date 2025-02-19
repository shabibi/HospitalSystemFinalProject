﻿using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Repositories;

namespace HospitalSystemTeamTask.Services
{
    public class BranchDepartmentService : IBranchDepartmentService
    {
        private readonly IBranchDepartmentRepo _branchDepartmentRepo;
        private readonly IBranchService _branchService;
        private readonly IDepartmentService _departmentService;

        public BranchDepartmentService(IBranchDepartmentRepo branchDepartmentRepo, IBranchService branchService, IDepartmentService departmentService)
        {
            _branchDepartmentRepo = branchDepartmentRepo;
            _branchService = branchService;
            _departmentService = departmentService;
        }

        public void AddDepartmentToBranch(BranchDepDTO department)
        {
            // Get all departments
            var departments = _departmentService.GetAllDepartments();

            // Validate if the department exists and is active
            var departmentExists = departments.Any(d => d.DepId == department.DID && d.DepartmentStatus);
            if (!departmentExists)
                throw new Exception("The specified department does not exist or is not active.");

            // Get all branches
            var branches = _branchService.GetAllBranches();

            // Validate if the branch exists and is active
            var branchExists = branches.Any(b => b.BID == department.BID && b.IsActive);
            if (!branchExists)
                throw new Exception("The specified branch does not exist or is not active.");

            // Create a new BranchDepartment object to associate the department with the branch
            var newBranchDep = new BranchDepartment
            {
                DepID = department.DID,
                BID = department.BID,
                DepartmentCapacity = 0 //calculated after asigning clinic 
            };

            // Add the department to the branch
            _branchDepartmentRepo.AddDepartmentToBranch(newBranchDep);
        }

        public IEnumerable<DepDTO>GetDepartmentsByBranch(int bid)
        {
            var branch = _branchService.GetBranchById(bid);
            if (branch == null || !branch.BranchStatus)
                throw new Exception($"{branch.BranchName} Not Found");

           var departments = _branchDepartmentRepo.GetDepartmentsByBranch(bid);
            List<DepDTO> result = new List<DepDTO>();
            foreach (var department in departments)
            {
                result.Add(new DepDTO
                {
                    DepartmentName = department.DepartmentName,
                    DepartmentStatus = department.IsActive,
                    DepId = department.DepID ,
                    Description = department.Description
                });
            }
            return result;
        }

        public IEnumerable<Branch> GetBranchsByDepartment(int did)
        {
            var department = _departmentService.GetDepartmentByid(did);
            if (department == null || !department.DepartmentStatus)
                throw new Exception($"{department.DepartmentName} Not Found");

            return _branchDepartmentRepo.GetBranchByDepartments(did);
       
             
        }
        public void UpdateBranchDepartment(BranchDepartment branchDepartment)
        {
            _branchDepartmentRepo.UpdateBranchDepartment(branchDepartment);
        }
        public BranchDepartment GetBranchDep(int departmentId, int branchId)
        {
           return _branchDepartmentRepo.GetBranchDep(departmentId, branchId);
        }

        public IEnumerable<DepDTO> GetDepartmentsByBranchName(string branchName)
        {
            var branch = _branchService.GetBranchDetailsByBranchName(branchName);
            if (branch == null || !branch.IsActive)
                throw new Exception($"{branch.BranchName} Not Found");

            var departments = _branchDepartmentRepo.GetDepartmentsByBranch(branch.BID);
            List<DepDTO> result = new List<DepDTO>();
            foreach (var department in departments)
            {
                result.Add(new DepDTO
                {
                    DepartmentName = department.DepartmentName,
                    DepartmentStatus = department.IsActive,
                    DepId = department.DepID,
                    Description = department.Description
                });
            }
            return result;
        }
    }
}
