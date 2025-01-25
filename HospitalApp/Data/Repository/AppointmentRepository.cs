using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAllWithDetails()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .ThenInclude(p => p.User)
                .Where(a => a.DoctorId == doctorId)
                .ToList();
        }

        public IEnumerable<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .ThenInclude(d => d.User)
                .Where(a => a.PatientId == patientId)
                .ToList();
        }

        public void UpdateStatus(int id, string status)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = status;
                _context.SaveChanges();
            }
        }

        public bool IsSlotAvailable(int doctorId, DateOnly date, string timeSlot)
        {
            return !_context.Appointments
                .Any(a => a.DoctorId == doctorId && a.Date == date && a.TimeSlot == timeSlot);
        }

    }
}
