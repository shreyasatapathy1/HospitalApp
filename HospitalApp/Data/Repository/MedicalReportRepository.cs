using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data.Repository
{
    public class MedicalReportRepository : Repository<GenerateReport>, IMedicalReportRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalReportRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<GenerateReport> GetReportsByDoctorId(int doctorId)
        {
            return _context.GenerateReports
                .Include(r => r.Appointment)
                .ThenInclude(a => a.Patient)
                .Where(r => r.Appointment.DoctorId == doctorId)
                .ToList();
        }
    }
}
