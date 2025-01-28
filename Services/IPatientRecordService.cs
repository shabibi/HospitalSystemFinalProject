using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IPatientRecordService
    {
        IEnumerable<PatientRecordOutput> GetAllRecords();
        void CreateRecord(PatientRecordInputDTO record, int doctorId);
        void UpdateRecord(int rid, string? treatment, string? inspection, int doctorId);
        void DeleteRecord(int rid);
        public IEnumerable<PatientRecordOutput> GetRecords(int patientId);
    }
}