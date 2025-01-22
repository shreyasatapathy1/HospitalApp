using HospitalApp.Models;

namespace HospitalApp.Data.Repository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        IEnumerable<Patient> GetAll();

    }
}

