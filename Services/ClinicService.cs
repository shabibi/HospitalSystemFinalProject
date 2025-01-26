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
        private readonly ILogger<ClinicService> _logger;

        public ClinicService(IClinicRepocs clinicRepo, IDoctorService doctorService, IBranchDepartmentService branchDepartmentService, ILogger<ClinicService> logger)
        {
            _clinicRepo = clinicRepo;
            _doctorService = doctorService;
            _branchDepartmentService = branchDepartmentService;
            _logger = logger;
        }

        public IEnumerable<Clinic> GetAllClinic()
        {
            _logger.LogInformation("Fetching all clinics at {Time}", DateTime.Now);
            return _clinicRepo.GetAllClinic();
        }

        // Method to add a new clinic
        public void AddClinic(ClinicInput input)
        {
            if (input == null)
            {
                _logger.LogError("Clinic details are null at {Time}", DateTime.Now);
                throw new ArgumentException("Clinic details are required.", nameof(input));
            }

            _logger.LogInformation("Validating clinic input at {Time}", DateTime.Now);
            ValidateClinicInput(input);

            _logger.LogInformation("Fetching and validating assigned doctor at {Time}", DateTime.Now);
            var doctor = GetAndValidateDoctor(input.AssignDoctor);

            TimeSpan totalDuration = input.EndTime - input.StartTime;
            int capacity = (int)(totalDuration.TotalMinutes / input.SlotDuration);

            var clinic = new Clinic
            {
                ClincName = input.ClincName.ToLower(),
                Capacity = capacity,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                SlotDuration = input.SlotDuration,
                Cost = input.Cost,
                DepID = doctor.DepId,
                BID = doctor.CurrentBrunch,
                AssignDoctor = input.AssignDoctor,
                IsActive = true
            };

            _logger.LogInformation("Adding clinic to the repository at {Time}", DateTime.Now);
            _clinicRepo.AddClinic(clinic);

            doctor.CID = clinic.CID;
            _logger.LogInformation("Updating doctor with clinic assignment at {Time}", DateTime.Now);
            _doctorService.UpdateDoctor(doctor);

            _logger.LogInformation("Clinic added successfully at {Time}", DateTime.Now);
        }

        private void ValidateClinicInput(ClinicInput input)
        {
            if (input.StartTime >= input.EndTime)
            {
                _logger.LogError("Invalid start and end times at {Time}", DateTime.Now);
                throw new ArgumentException("Start time cannot be later than End time.");
            }
            if (input.SlotDuration <= 0)
            {
                _logger.LogError("Invalid slot duration at {Time}", DateTime.Now);
                throw new ArgumentException("Slot duration must be a positive number.");
            }
        }

        private Doctor GetAndValidateDoctor(int doctorId)
        {
            _logger.LogInformation("Fetching doctor with ID {DoctorId} at {Time}", doctorId, DateTime.Now);
            var doctor = _doctorService.GetDoctorById(doctorId);
            if (doctor == null)
            {
                _logger.LogError("Doctor with ID {DoctorId} not found at {Time}", doctorId, DateTime.Now);
                throw new ArgumentException("Doctor not found.", nameof(doctorId));
            }
            return doctor;
        }

        private void UpdateBranchDepartmentCapacity(int depId, int branchId, int capacity)
        {
            _logger.LogInformation("Updating branch and department capacity at {Time}", DateTime.Now);
            var branchDepartment = _branchDepartmentService.GetBranchDep(depId, branchId);
            branchDepartment.DepartmentCapacity += capacity;
            _branchDepartmentService.UpdateBranchDepartment(branchDepartment);
            _logger.LogInformation("Branch and department capacity updated successfully at {Time}", DateTime.Now);
        }

        public Clinic GetClinicById(int clinicId)
        {
            if (clinicId <= 0)
            {
                _logger.LogError("Invalid clinic ID {ClinicId} at {Time}", clinicId, DateTime.Now);
                throw new ArgumentException("Invalid clinic ID. Clinic ID must be greater than 0.");
            }

            _logger.LogInformation("Fetching clinic with ID {ClinicId} at {Time}", clinicId, DateTime.Now);
            var clinic = _clinicRepo.GetClinicById(clinicId);

            if (clinic == null)
            {
                _logger.LogError("Clinic with ID {ClinicId} not found at {Time}", clinicId, DateTime.Now);
                throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");
            }

            return clinic;
        }

        public decimal GetPrice(int clinicId)
        {
            _logger.LogInformation("Fetching price for clinic with ID {ClinicId} at {Time}", clinicId, DateTime.Now);
            var clinic = GetClinicById(clinicId);
            return clinic.Cost;
        }

        public Clinic GetClinicByName(string clinicName)
        {
            _logger.LogInformation("Fetching clinic with name {ClinicName} at {Time}", clinicName, DateTime.Now);
            var clinic = _clinicRepo.GetClinicByName(clinicName.ToLower());
            if (clinic == null)
            {
                _logger.LogError("Clinic with name {ClinicName} not found at {Time}", clinicName, DateTime.Now);
                throw new KeyNotFoundException($"Clinic with name {clinicName} not found.");
            }
            return clinic;
        }

        public IEnumerable<Clinic> GetClinicsByBranchName(string branchName)
        {
            if (string.IsNullOrEmpty(branchName))
            {
                _logger.LogError("Branch name is null or empty at {Time}", DateTime.Now);
                throw new ArgumentException("Branch name is required.");
            }

            _logger.LogInformation("Fetching clinics for branch {BranchName} at {Time}", branchName, DateTime.Now);
            var clinics = _clinicRepo.GetClinicsByBranchName(branchName.ToLower());
            if (!clinics.Any())
            {
                _logger.LogError("No clinics found for branch {BranchName} at {Time}", branchName, DateTime.Now);
                throw new KeyNotFoundException($"No clinics found for branch: {branchName}");
            }

            return clinics;
        }

        public IEnumerable<Clinic> GetClinicsByDepartmentId(int departmentId)
        {
            if (departmentId <= 0)
            {
                _logger.LogError("Invalid department ID {DepartmentId} at {Time}", departmentId, DateTime.Now);
                throw new ArgumentException("Department ID must be greater than 0.");
            }

            _logger.LogInformation("Fetching clinics for department ID {DepartmentId} at {Time}", departmentId, DateTime.Now);
            var clinics = _clinicRepo.GetClinicsByDepartmentID(departmentId);
            if (!clinics.Any())
            {
                _logger.LogError("No clinics found for department ID {DepartmentId} at {Time}", departmentId, DateTime.Now);
                throw new KeyNotFoundException($"No clinics found for Department ID: {departmentId}");
            }

            return clinics;
        }

        public void UpdateClinicDetails(int CID, ClinicInput input)
        {
            _logger.LogInformation("Updating clinic details for clinic ID {ClinicId} at {Time}", CID, DateTime.Now);
            var existingClinic = _clinicRepo.GetClinicById(CID);
            TimeSpan totalDuration = input.EndTime - input.StartTime;
            if (input.SlotDuration <= 0)
            {
                _logger.LogError("Invalid slot duration for clinic ID {ClinicId} at {Time}", CID, DateTime.Now);
                throw new ArgumentException("Capacity must be greater than 0.");
            }

            int Capacity = (int)(totalDuration.TotalMinutes / input.SlotDuration);
            var doctor = _doctorService.GetDoctorById(input.AssignDoctor);

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

            _clinicRepo.UpdateClinic(existingClinic);
            _logger.LogInformation("Clinic details updated successfully for clinic ID {ClinicId} at {Time}", CID, DateTime.Now);
        }

        public void SetClinicStatus(int clinicId)
        {
            if (clinicId <= 0)
            {
                _logger.LogError("Invalid clinic ID {ClinicId} at {Time}", clinicId, DateTime.Now);
                throw new ArgumentException("Invalid Clinic ID.");
            }

            _logger.LogInformation("Fetching clinic for status update with ID {ClinicId} at {Time}", clinicId, DateTime.Now);
            var clinic = _clinicRepo.GetClinicById(clinicId);
            if (clinic == null)
            {
                _logger.LogError("Clinic with ID {ClinicId} not found at {Time}", clinicId, DateTime.Now);
                throw new KeyNotFoundException($"Clinic with ID {clinicId} not found.");
            }

            clinic.IsActive = false;
            _clinicRepo.UpdateClinic(clinic);
            _logger.LogInformation("Clinic status updated to inactive for clinic ID {ClinicId} at {Time}", clinicId, DateTime.Now);
        }

        public string GetClinicName(int cid)
        {
            _logger.LogInformation("Fetching clinic name for clinic ID {ClinicId} at {Time}", cid, DateTime.Now);
            return _clinicRepo.GetClinicName(cid);
        }

        public IEnumerable<Clinic> GetClinicByBranchDep(int bid, int depid)
        {
            _logger.LogInformation("Fetching clinics for branch ID {BranchId} and department ID {DepartmentId} at {Time}", bid, depid, DateTime.Now);
            return _clinicRepo.GetClinicByBranchDep(bid, depid);
        }
    }
}