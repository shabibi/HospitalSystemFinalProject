using HospitalSystemTeamTask.DTO_s;
using HospitalSystemTeamTask.Models;

namespace HospitalSystemTeamTask.Services
{
    public interface IBookingService
    {
        void BookAppointment(BookingInputDTO input, int patientId);
        void CancelAppointment(BookingInputDTO bookingInputDTO, int patientId);
        void DeleteAppointments(BookingInputDTO bookingInputDTO);
        IEnumerable<BookingOutputDTO> GetAllBooking();
        IEnumerable<BookingInputDTO> GetAvailableAppointmentsBy(int? clinicId, int? departmentId);
        IEnumerable<BookingOutputDTO> GetBookedAppointments(int patientId);
        Booking GetBookingById(int bookingId);
        IEnumerable<Booking> GetBookingsByClinicAndDate(int clinicId, DateTime date);
        IEnumerable<Booking> ScheduledAppointments(int cid, DateTime appointmentDate);
        void UpdateBookedAppointment(BookingInputDTO previousAppointment, BookingInputDTO newAppointment, int patientId);
    }
}