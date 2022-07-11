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
    public class ReservationRepository : IReservationRepository
    {

        private readonly DataBaseContext _dataBaseContext;


        public ReservationRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;


        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await (from user in _dataBaseContext.Users
                          join reservation in _dataBaseContext.Reservations on user.Id equals reservation.UserId
                          join workSpace in _dataBaseContext.WorkSpaces on reservation.SpaceId equals workSpace.Id
                          select new Reservation
                          {
                              Id = reservation.Id,
                              SpaceId = reservation.SpaceId,
                              Date = reservation.Date,
                              UserId = reservation.UserId,
                              User = user,
                              WorkSpace = workSpace

                          }).ToListAsync();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await (from user in _dataBaseContext.Users
                          join reservation in _dataBaseContext.Reservations on user.Id equals reservation.UserId
                          join workSpace in _dataBaseContext.WorkSpaces on reservation.SpaceId equals workSpace.Id
                          select new Reservation
                          {
                              Id = reservation.Id,
                              SpaceId = reservation.SpaceId,
                              Date = reservation.Date,
                              UserId = reservation.UserId,
                              User = user,
                              WorkSpace = workSpace

                          }).FirstOrDefaultAsync(x => x.Id == id);


        }

       
        public async Task<Reservation> Create(Reservation reservation)
        {
            var user = _dataBaseContext.Users.Where(x => x.Id == reservation.UserId).FirstOrDefault();

            var workSpace = _dataBaseContext.WorkSpaces.Where(x => x.Id == reservation.SpaceId).FirstOrDefault();

            var now = ((TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"))));

            var spaceId = reservation.SpaceId;

            var userId = reservation.UserId;

            var dateUser = reservation.Date;

            var days = (dateUser.Day - now.Day);


            var haveReservation = (from r in _dataBaseContext.Reservations
                                   where r.UserId == userId && r.Date == dateUser
                                   select r).Any();

            var ocupated = (from r in _dataBaseContext.Reservations
                            where r.SpaceId == spaceId && r.Date == dateUser
                            select r).Any();

            if (haveReservation == false)
            {
                if (ocupated == false)
                {

                    if ((user.Company == "bitwork" && workSpace.Company == "bitwork") || (user.Company == "marketing" && workSpace.Company == "marketing"))
                    {
                        if (now.DayOfWeek == DayOfWeek.Thursday)
                        {
                            if (days < 5 && days > 3 || days < 2 && days > 0)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                        if (now.DayOfWeek == DayOfWeek.Friday)
                        {
                            if (days < 5 && days > 2)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                        else if (now.DayOfWeek == DayOfWeek.Saturday)
                        {
                            if (days < 4 && days > 1)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            if (days < 3 && days > 0)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                    }
                    else if (user.Company == "bravent")
                    {
                        if (now.DayOfWeek == DayOfWeek.Friday)
                        {
                            if (days < 4 && days > 2)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                        else if (now.DayOfWeek == DayOfWeek.Saturday)
                        {
                            if (days < 3 && days > 1)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            if (days < 2 && days > 0)
                            {
                                _dataBaseContext.Add(reservation);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                    }
                }


            }

            return reservation;
        }

       
    }
}
