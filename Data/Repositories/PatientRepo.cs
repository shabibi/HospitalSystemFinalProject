﻿
using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Repositories;
using Microsoft.EntityFrameworkCore;
using HospitalSystemTeamTask.Data.Models;

namespace HospitalSystemTeamTask.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        public ApplicationDbContext _context;
        public PatientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get All Patients
        public IEnumerable<Patient> GetAllPatients()
        {
            try
            {
                return _context.Patients.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        //Get Patients by id
       public Patient GetPatientsById(int PID)
{
    try
    {
        return _context.Patients.Include(p => p.User).FirstOrDefault(p => p.PID == PID)
            ?? throw new KeyNotFoundException($"Patient with ID {PID} not found.");
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException($"Database error: {ex.Message}");
    }
}



        public void UpdatePatient(Patient patient)
        {
            try
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }


        }

        public void AddPatient(Patient patient)
        {
            try
            {
          
                // Add the Patient entity
                _context.Patients.Add(patient);

                // Save changes to the database
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

        public Patient GetPatientByName(string PatientName)
        {
            try
            {
                return _context.Patients
                    .Include(p => p.User) 
                    .FirstOrDefault(p => p.User.UserName == PatientName); 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Database error: {ex.Message}");
            }
        }

    }

}

