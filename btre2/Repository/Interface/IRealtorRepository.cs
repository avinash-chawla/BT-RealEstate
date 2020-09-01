using btre2.Models;
using btre2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace btre2.Repository.Interface
{
    public interface IRealtorRepository
    {
        Realtor GetRealtor(int id);
        IEnumerable<Realtor> GetRealtors();
        string CreateRealtor(CreateRealtorViewModel model);
        Realtor Update(Realtor realtor);
        string Delete(int id);
    }
}
