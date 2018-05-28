using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IGameRepository
    {
        IEnumerable<Games> GetAll();

        Task<Games> Find(int id);

        Task<bool> Exist(int id);

    }
}
