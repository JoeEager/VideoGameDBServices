using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturers> GetAll();

        Task<Manufacturers> Find(int id);

        Task<bool> Exist(int id);

    }
}
