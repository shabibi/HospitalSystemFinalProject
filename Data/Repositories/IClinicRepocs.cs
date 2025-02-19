﻿using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Repositories
{
    public interface IClinicRepocs
    {
        public IEnumerable<Clinic> GetAllClinic();
        void AddClinic(Clinic clinic);
        Clinic GetClinicById(int Cid);
        Clinic GetClinicByName(string ClinicName);
        IEnumerable<Clinic> GetClinicsByBranchName(string branchName);
        IEnumerable<Clinic> GetClinicsByDepartmentID(int depId);
        void UpdateClinic(Clinic clinic);
        string GetClinicName(int cid);
        IQueryable<Clinic> GetClinicByBranchDep(int bid, int depid);
    }
}
