using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IYearRepository
    {
        IEnumerable<Years> GetAll();

        Task<Years> Find(int id);

        Task<bool> Exist(int id);

        Task<List<Games>> GetGamesByYear(int yearId);
    }
}
