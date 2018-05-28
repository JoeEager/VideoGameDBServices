using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IRatingRepository
    {
        IEnumerable<Ratings> GetAll();

        Task<Ratings> Find(int id);

        Task<bool> Exist(int id);

    }
}
