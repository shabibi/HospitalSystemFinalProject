using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

using HospitalSystemTeamTask.Repositories;


namespace HospitalSystemTeamTask.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepocs _clinicRepo;
        private readonly IDoctorService _doctorService;

        private readonly IBranchDepartmentService _branchDepartmentService;



        public ClinicService(IClinicRepocs clinicRepo,  IDoctorService doctorService, IBranchDepartmentService branchDepartmentService)
        {
            _clinicRepo = clinicRepo;
   
            _doctorService = doctorService;
            _branchDepartmentService = branchDepartmentService;
        }
        public IEnumerable<Clinic> GetAllClinic()
        {
            return _clinicRepo.GetAllClinic();
        }


        // Method to add a new clinic
        public void AddClinic(ClinicInput input)
        {
            if (input == null)
            {
                throw new ArgumentException("Clinic details are required.", nameof(input));
            }

            // Validate the input (You can add custom validation logic here)
            ValidateClinicInput(input);

            // Fetch and validate the doctor assigned to the clinic
            var doctor = GetAndValidateDoctor(input.AssignDoctor);

            // Calculate the capacity based on slot duration and start/end times
            TimeSpan totalDuration = input.EndTime - input.StartTime;
            int capacity = (int)(totalDuration.TotalMinutes / input.SlotDuration);

            // Create the clinic object
            var clinic = new Clinic
            {
                ClincName = input.ClincName.ToLower(), // Clinic name in lowercase
                Capacity = capacity,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                SlotDuration = input.SlotDuration,
                Cost = input.Cost,
                DepID = doctor.DepId, // Department ID from doctor info
                BID = doctor.CurrentBrunch, // Branch ID from doctor info
                AssignDoctor = input.AssignDoctor,
                IsActive = true // Clinic is active by default
            };

            // Save the clinic in the repository (database or in-memory storage)

            // Update the branch and department capacity after clinic is added
            UpdateBranchDepartmentCapacity(doctor.DepId, doctor.CurrentBrunch, capacity);
        
            _clinicRepo.AddClinic(clinic);
            doctor.CID = clinic.CID;

            _doctorService.UpdateDoctor(doctor); // Update the doctor's record
                                                 // Assign the clinic ID to the doctor

        }

        // Validation logic for clinic input
        private void ValidateClinicInput(ClinicInput input)
        {
            // Implement any specific validation logic here, for example:
            if (input.StartTime >= input.EndTime)
            {
                throw new ArgumentException("Start time cannot be later than End time.");
            }
            if (input.SlotDuration <= 0)
            {
                throw new ArgumentException("Slot duration must be a positive number.");
            }
        }

        // Fetch and validate the doctor assigned to the clinic
        private Doctor GetAndValidateDoctor(int doctorId)
        {
            var doctor = _doctorService.GetDoctorById(doctorId);
            if (doctor == null)
            {
                throw new ArgumentException("Doctor not found.", nameof(doctorId));
            }
            return doctor;
        }

        // Update the capacity of the branch and department after a clinic is added
      
        private void UpdateBranchDepartmentCapacity(int depId, int branchId, int capacity)
        {
            var branchDepartment = _branchDepartmentService.GetBranchDep(depId, branchId);
            branchDepartment.DepartmentCapacity += capacity;
            _branchDepartmentService.UpdateBranchDepartment(branchDepartment);
        }


        public Clinic GetClinicById(int clinicId)
        {
            // Validate input
            if (clinicId <= 0)
            {
                throw new ArgumentException("Invalid clinic ID. Clinic ID must be greater than 0.");
            }

            // Retrieve clinic by ID
            var clinic = _clinicRepo.GetClinicById(clinicId);

            // Handle clinic not found
            if (clinic == null)
            {
                throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");
            }

            return clinic;
        }

        public decimal GetPrice(int clinicId)
        {
            var clinic = GetClinicById(clinicId);
            return clinic.Cost;
        }

        //public Clinic GetClinicByDoctor (int doctorId)
        //{
            
        //}
        public Clinic GetClinicByName(string clinicName)
        {
            var clinic = _clinicRepo.GetClinicByName(clinicName.ToLower());
            if (clinic == null)
                throw new KeyNotFoundException($"clinic with name {clinicName} not found.");
            return clinic;
        }

        public IEnumerable<Clinic> GetClinicsByBranchName(string branchName)
        {
            if (string.IsNullOrEmpty(branchName))
            {
                throw new ArgumentException("Branch name is required.");
            }

            var clinics = _clinicRepo.GetClinicsByBranchName(branchName.ToLower());
            if (!clinics.Any())
            {
                throw new KeyNotFoundException($"No clinics found for branch: {branchName}");
            }

            return clinics;
        }

        public IEnumerable<Clinic> GetClinicsByDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
            {
                throw new ArgumentException("Department ID must be greater than 0.");
            }

            var clinics = _clinicRepo.GetClinicsByDepartmentID(departmentId);
            if (!clinics.Any())
            {
                throw new KeyNotFoundException($"No clinics found for Department ID: {departmentId}");
            }

            return clinics;
        }

        public void UpdateClinicDetails( int CID,ClinicInput input)
        {
            var existingClinic = _clinicRepo.GetClinicById(CID);
            TimeSpan totalDuration = input.EndTime - input.StartTime;
            if (input.SlotDuration <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0.");
            }

            int Capacity = (int)(totalDuration.TotalMinutes / input.SlotDuration);
            var doctor = _doctorService.GetDoctorById(input.AssignDoctor);

            // Map updated properties
            existingClinic.ClincName = input.ClincName;
            existingClinic.Capacity = Capacity;
            existingClinic.StartTime = input.StartTime;
            existingClinic.EndTime = input.EndTime;
            existingClinic.SlotDuration = input.SlotDuration;
            existingClinic.Cost = input.Cost;
            existingClinic.IsActive = true;
            existingClinic.DepID = doctor.DepId;
            existingClinic.BID = doctor.CurrentBrunch;
            existingClinic.AssignDoctor = input.AssignDoctor;
            

            // Persist changes
            _clinicRepo.UpdateClinic(existingClinic);
        }

        public void SetClinicStatus(int clinicId)
        {
            if (clinicId <= 0)
            {
                throw new ArgumentException("Invalid Clinic ID.");
            }

            // Fetch the clinic
            var clinic = _clinicRepo.GetClinicById(clinicId);
            if (clinic == null)
            {
                throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");
            }

            // Update the IsActive flag
            clinic.IsActive = false;

            // Persist changes
            _clinicRepo.UpdateClinic(clinic);
        }

        public string GetClinicName(int cid)
        {
            return _clinicRepo.GetClinicName(cid);
        }

        public IEnumerable<Clinic> GetClinicByBranchDep(int bid,int depid)
        {
           return _clinicRepo.GetClinicByBranchDep(bid, depid);
        }
    }

}

