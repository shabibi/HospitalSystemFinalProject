﻿using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IDepartmentService
    {
        void CreateDepartment(DepartmentDTO departmentDto);
        IEnumerable<DepDTO> GetAllDepartments();
        void UpdateDepartment(DepDTO departmentDto);
        void SetDepartmentActiveStatus(int departmentId, bool isActive);
        Department GetDepartmentByName(string department);
       DepDTO GetDepartmentByid(int did);
        string GetDepartmentName(int depId);

    }
}