
using HospitalApp.Models;
using HospitalApp.Models.ViewModel;

namespace HospitalApp.Data.Repository
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        IEnumerable<Appointment> GetAllWithDetails();
        IEnumerable<Appointment> GetAppointmentsByDoctorId(int doctorId);
        IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId);
        IEnumerable<AdminAppointmentViewModel> GetAllAppointmentsWithDetails();

        Appointment GetAppointmentWithDetails(int appointmentId);

        void UpdateStatus(int id, string status);
        bool IsSlotAvailable(int doctorId, DateOnly date, string timeSlot);

        

    }
}
