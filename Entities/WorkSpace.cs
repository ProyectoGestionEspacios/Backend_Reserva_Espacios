using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WorkSpace
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Company { get; set; }


        public List<Reservation> Reservations { get; set; }

    }
}
