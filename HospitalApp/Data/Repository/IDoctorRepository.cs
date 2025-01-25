using HospitalApp.Models;

namespace HospitalApp.Data.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        IEnumerable<Doctor> GetAll();
        IEnumerable<Doctor> FindBySpecialization(string specialization);
        Doctor GetById(int id);
    }
}
