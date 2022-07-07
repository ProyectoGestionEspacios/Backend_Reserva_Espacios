using Entities;
using Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class WorkSpaceRepository : IWorkSpaceRepository
    {
        private readonly DataBaseContext _dataBaseContext;


        public WorkSpaceRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;

        }

        public async Task<IEnumerable<WorkSpace>> GetAllGrouped()
        {
            return await (from workSpace in _dataBaseContext.WorkSpaces
                          join reservation in _dataBaseContext.Reservations on workSpace.Id equals reservation.SpaceId
                          select new WorkSpace
                          {
                              Id = workSpace.Id,
                              Name = workSpace.Name,
                              Company = workSpace.Company,

                              Reservations = new List<Reservation> { reservation }


                          }).ToListAsync();
        }

        public async Task<IEnumerable<WorkSpace>> GetAll()
        {
            return await (from workSpace in _dataBaseContext.WorkSpaces
                          select new WorkSpace
                          {
                              Id = workSpace.Id,
                              Name = workSpace.Name,
                              Company = workSpace.Company,



                          }).ToListAsync();
        }
    }
}
