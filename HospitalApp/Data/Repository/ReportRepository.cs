using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data.Repository
{
    public class ReportRepository : Repository<GenerateReport>, IReportRepository
    {
        private readonly ApplicationDbContext _context;
        
        public ReportRepository(ApplicationDbContext context) : base(context) 
        { 
            _context = context;
        
        }

        public IEnumerable<GenerateReport> GetReportsByDoctorId(int doctorId)
        {
            return _context.GenerateReports
                .Include(r => r.Appointment)
                .ThenInclude(a => a.Doctor)
                .Where(r => r.Appointment.DoctorId == doctorId)
                .ToList();
        }

        public IEnumerable<GenerateReport> GetReportsByPatientId(int patientId)
        {
            return _context.GenerateReports
                .Include(r => r.Appointment)
                .ThenInclude(a => a.Patient)
                .Where(r => r.Appointment.PatientId == patientId)
                .ToList();
        }
    }
}
