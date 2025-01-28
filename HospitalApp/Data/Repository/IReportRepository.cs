using HospitalApp.Models;

namespace HospitalApp.Data.Repository
{
    public interface IReportRepository : IRepository<GenerateReport>
    {
        IEnumerable<GenerateReport> GetReportsByDoctorId(int doctorId);
        IEnumerable<GenerateReport> GetReportsByPatientId(int patientId);
        
    }
}
