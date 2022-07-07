using Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SpaceId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public ApplicationUser? User { get; set; }

        public WorkSpace? WorkSpace { get; set; }

    }
}
