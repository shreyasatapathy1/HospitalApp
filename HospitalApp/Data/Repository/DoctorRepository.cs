using HospitalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Data.Repository
{
    public class DoctorRepository : Repository<Doctor> , IDoctorRepository  {
        private readonly ApplicationDbContext _context;
        public DoctorRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;

        }
        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.Include(p => p.User).ToList();
        }
        public Doctor GetById(int id)
        {
            return _context.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);

        }




    }
}
