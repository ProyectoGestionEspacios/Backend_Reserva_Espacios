using Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Meeting
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime StarHour { get; set; }

        public DateTime EndHour { get; set; }

        public string Description { get; set; }

        public ApplicationUser? User { get; set; }

    }
}
