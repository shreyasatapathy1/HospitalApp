using HospitalApp.Models;

namespace HospitalApp.Data.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<Doctor> GetAll();

        Doctor GetById(int id);
    }
}
