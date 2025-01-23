using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Repositories;

namespace HospitalSystemTeamTask.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<DepDTO > GetAllDepartments()
        {
            return _departmentRepository.GetAllDepartments()
                .Select(dept => new DepDTO
                {
                    DepId = dept.DepID,
                    DepartmentName = dept.DepartmentName,
                    Description = dept.Description,
                    DepartmentStatus = dept.IsActive
                   
                }).ToList();
        }

        public void CreateDepartment(DepartmentDTO departmentDto)
        {
            var department = new Department
            {
                DepartmentName = departmentDto.DepartmentName.ToLower(),
                Description = departmentDto.Description,
                IsActive = true // Default new departments to active
            };

            _departmentRepository.AddDepartment(department);
        }

        public void UpdateDepartment(DepDTO departmentDto)
        {
            var updatedDepartment = new Department
            {
                DepartmentName = departmentDto.DepartmentName,
                Description = departmentDto.Description,
                IsActive = departmentDto.DepartmentStatus
            };

            _departmentRepository.UpdateDepartment(departmentDto.DepId, updatedDepartment);
        }

        public void SetDepartmentActiveStatus(int departmentId, bool isActive)
        {
            var department = _departmentRepository.GetDepartmentById(departmentId);
            if (department != null)
            {
                department.IsActive = isActive;
                _departmentRepository.UpdateDepartment(departmentId, department);
            }
        }

        public Department GetDepartmentByName(string department)
        {
           return _departmentRepository.GetDepartmentByName(department);
        }
       
        public DepDTO GetDepartmentByid(int departmentId)
        {
            var department = _departmentRepository.GetDepartmentById(departmentId);

            if (department == null) return null;

            return new DepDTO
            {
                DepId = department.DepID,
                DepartmentName = department.DepartmentName,
                Description = department.Description,
                DepartmentStatus = department.IsActive
            };
        }

    }
}
