﻿
using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;
using HospitalSystemTeamTask.Repositories;
using HospitalSystemTeamTask.Shared.Helper;
using System.Security.Cryptography;


namespace HospitalSystemTeamTask.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _PatientRepo;
        private readonly IUserService _userService;
        private readonly ISendEmail _sendEmail;

        public PatientService(IPatientRepo PatientRepo, IUserService userService, ISendEmail sendEmail)
        {
            _PatientRepo = PatientRepo;
            _userService = userService;
            _sendEmail = sendEmail;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _PatientRepo.GetAllPatients();
        }

        public Patient GetPatientById(int PID)
        {
            var patient = _PatientRepo.GetPatientsById(PID);

            if (patient == null)
            {
                throw new KeyNotFoundException($"Patient with ID {PID} not found.");
            }

            return patient;
        }


        public PatienoutputDTO GetPatientData(string? userName, int? Pid)
        {
            Patient patient = null;

            // Validate that at least one parameter is provided
            if (string.IsNullOrWhiteSpace(userName) && !Pid.HasValue)
            {
                throw new ArgumentException("Either username or patient ID must be provided.");
            }

            // Retrieve user based on username
            if (!string.IsNullOrEmpty(userName))
            {
                patient = GetPatientByName(userName);
            }

            // Retrieve user based on PID if username is not provided or no match was found
            if (patient == null && Pid.HasValue)
            {
                patient = GetPatientById(Pid.Value);
            }

            // Validate if the patient exists
            if (patient == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }

            // Prepare the output DTO
            var outputData = new PatienoutputDTO
            {
                PID = patient.PID,
                UserName = patient.User.UserName.ToLower(),
                Email = patient.User.Email,
                Phone = patient.User.Phone,
                Role = patient.User.Role,
                IsActive = patient.User.IsActive
            };

            return outputData;
        }


        public Patient GetPatientByName(string PatientName)
        {
            var patient = _PatientRepo.GetPatientByName(PatientName.ToLower());
            if (patient == null)
                throw new KeyNotFoundException($"User with Name {PatientName} not found.");
            return patient;
        }


        public void UpdatePatientDetails( int UID, string phoneNumber)
        {
           
            //get patient data from user table
            var existingUser = _userService.GetUserById(UID);

            //update patient data (only accept to update phone number)
            existingUser.Phone = phoneNumber;

            if (existingUser == null)
            {
                throw new KeyNotFoundException("Patient not found.");
            }
           
            // Validate and update the user's name and phone number
            if (!string.IsNullOrEmpty(phoneNumber) &&
                int.TryParse(phoneNumber, out int parsedPhone) &&
                phoneNumber.Length == 8)
            {
                existingUser.Phone = parsedPhone.ToString(); // Update phone as a string after validation
            }
            else
            {
                throw new ArgumentException("Invalid phone number. It must be exactly 8 numeric digits.");
            }

            _userService.UpdateUser(existingUser);
        }


        public void AddPatient(PatientInputDTO patientInput)
        {
            if (patientInput == null)
            {
                throw new ArgumentException("Patient or User information is missing.");
            }

            // Validate Gender (accept M, m, F, f)
            if (string.IsNullOrWhiteSpace(patientInput.Gender) ||
                !(patientInput.Gender.Equals("M", StringComparison.OrdinalIgnoreCase) ||
                  patientInput.Gender.Equals("F", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("Gender must be 'M' or 'F'.");
            }

            // Hash the password
            var hashedPassword = HashingPassword.Hshing(patientInput.Password);

            // Create a new user
            var user = new User
            {
                UserName = patientInput.UserName.ToLower(),
                Password = hashedPassword,
                Email = patientInput.Email,
                Role = "patient",
                IsActive = true,
                Phone = patientInput.Phone
            };
            _userService.AddUser(user);

            // Save patient details
            var patient = new Patient
            {
                PID = user.UID,
                Age = patientInput.Age,
                Gender = patientInput.Gender.ToUpper() // Normalize Gender to uppercase
            };

            // Send email notification
            string subject = "Hospital System Signing In";
            string body = $"Dear {patientInput.UserName},\n\nYour account has been created successfully for Hospital System.\n\nBest Regards,\nHospital System";

            _sendEmail.SendEmailAsync(patientInput.Email, subject, body);

            // Delegate to repository
            _PatientRepo.AddPatient(patient);
        }


    }


}


