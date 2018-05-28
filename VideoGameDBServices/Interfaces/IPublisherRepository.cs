using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDBServices.Models;

namespace VideoGameDBServices.Interfaces
{
    public interface IPublisherRepository
    {
        IEnumerable<Publishers> GetAll();

        Task<Publishers> Find(int id);

        Task<bool> Exist(int id);

        Task<List<Games>> GetGamesByPublisher(int publisherId);

    }
}
