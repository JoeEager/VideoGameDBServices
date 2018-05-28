using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface ISystemRepository
    {
        IEnumerable<Systems> GetAll();

        Task<Systems> Find(int id);

        Task<bool> Exist(int id);

        Task<List<Games>> GetGamesBySystem(int systemId);
    }
}
