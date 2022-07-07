using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public interface IWorkSpaceRepository
    {
        Task<IEnumerable<WorkSpace>> GetAllGrouped();

        Task<IEnumerable<WorkSpace>> GetAll();
    }
}
