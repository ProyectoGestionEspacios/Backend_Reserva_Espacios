using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();

        Task<Reservation> GetById(int id);

        Task<Reservation> Create(Reservation reservation);


    }
}
