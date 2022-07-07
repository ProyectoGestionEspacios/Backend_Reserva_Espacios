using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<Meeting>> GetAll();

        Task<Meeting> Create(Meeting meeting);

        Task<Meeting> GetById(int id);

    }
}
