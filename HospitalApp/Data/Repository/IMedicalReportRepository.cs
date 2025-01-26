using HospitalApp.Models;

namespace HospitalApp.Data.Repository
{
    public interface IMedicalReportRepository : IRepository<GenerateReport>
    {
        IEnumerable<GenerateReport> GetReportsByDoctorId(int doctorId);
        GenerateReport GetReportWithDetails(int appointmentId);

    }
}
