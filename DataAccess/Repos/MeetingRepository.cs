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
    public class MeetingRepository : IMeetingRepository
    {
        private readonly DataBaseContext _dataBaseContext;


        public MeetingRepository(DataBaseContext dataBaseContext)
        {
            _dataBaseContext = dataBaseContext;


        }

        public async Task<IEnumerable<Meeting>> GetAll()
        {
            return await (from user in _dataBaseContext.Users
                          join meeting in _dataBaseContext.Meeting on user.Id equals meeting.UserId
                          select new Meeting
                          {
                              Id = meeting.Id,
                              StartHour = meeting.StartHour,
                              EndHour = meeting.EndHour,
                              UserId = meeting.UserId,
                              User = user,

                          }).ToListAsync();
        }

        public async Task<Meeting> GetById(int id)
        {
            return await (from user in _dataBaseContext.Users
                          join meeting in _dataBaseContext.Meeting on user.Id equals meeting.UserId
                          select new Meeting
                          {
                              Id = meeting.Id,
                              StartHour = meeting.StartHour,
                              EndHour = meeting.EndHour,
                              UserId = meeting.UserId,
                              User = user,

                          }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Meeting> Create(Meeting meeting)
        {
            var user = _dataBaseContext.Users.Where(x => x.Id == meeting.UserId).FirstOrDefault();

            var now = ((TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"))));

            var userId = meeting.UserId;

            var dateStart = meeting.StartHour;

            var dateEnd = meeting.EndHour;

            var resultDay = (dateStart.Day - now.Day);

            var resultHour = (dateEnd.Hour - dateStart.Hour);

            var haveMeeting = (from m in _dataBaseContext.Meeting
                               where m.UserId == userId && m.StartHour == dateStart
                               select m).Any();

            var repeathour = (from m in _dataBaseContext.Meeting
                              where m.StartHour.Hour == dateStart.Hour && m.EndHour.Hour == dateEnd.Hour
                              select m).Any();

            if (haveMeeting == false)
            {
                if (repeathour == false)
                {

                    if (now.DayOfWeek == DayOfWeek.Friday)
                    {
                        if (resultDay < 4 && resultDay > 2)
                        {
                            if (resultHour <= 1)
                            {
                                _dataBaseContext.Add(meeting);
                                await _dataBaseContext.SaveChangesAsync();
                            }

                        }
                    }
                    else if (now.DayOfWeek == DayOfWeek.Saturday)
                    {
                        if (resultDay < 3 && resultDay > 1)
                        {
                            if (resultHour <= 1)
                            {
                                _dataBaseContext.Add(meeting);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                    }
                    else
                    {
                        if (resultDay < 2 && resultDay > 0)
                        {
                            if (resultHour <= 1)
                            {
                                _dataBaseContext.Add(meeting);
                                await _dataBaseContext.SaveChangesAsync();
                            }
                        }
                    }
                }

            }
            return meeting;

        }
    }
}
