using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IDeveloperRepository
    {
        IEnumerable<Developers> GetAll();

        Task<Developers> Find(int id);

        Task<bool> Exist(int id);


    }
}
