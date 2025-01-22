using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data.Repository
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.Include(p => p.User).ToList();
        }
    }
}
