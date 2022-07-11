using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataContext.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new DataBaseContext(serviceProvider.GetRequiredService<DbContextOptions<DataBaseContext>>()))
            {

                if (_context.WorkSpaces.Any())
                {
                    return;
                }
                _context.WorkSpaces.AddRange(
                    new WorkSpace { Name = "espacio Eris, puesto 1.", Company = "bitwork" },
                    new WorkSpace { Name = "espacio Eris, puesto 2.", Company = "bitwork" },
                    new WorkSpace { Name = "espacio Eris, puesto 3.", Company = "bitwork" },
                    new WorkSpace { Name = "espacio Eris, puesto 4.", Company = "bitwork" },

                    new WorkSpace { Name = "espacio Pluton, puesto 5.", Company = "marketing" },
                    new WorkSpace { Name = "espacio Pluton, puesto 6.", Company = "marketing" },
                    new WorkSpace { Name = "espacio Pluton, puesto 7.", Company = "marketing" },
                    new WorkSpace { Name = "espacio Pluton, puesto 8.", Company = "marketing" },
                    new WorkSpace { Name = "espacio Pluton, puesto 9.", Company = "marketing" },

                    new WorkSpace { Name = "espacio Ceres, puesto 10.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Ceres, puesto 11.", Company = "bravent" },

                    new WorkSpace { Name = "espacio Central, Ultron puesto 12.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ultron puesto 13.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ultron puesto 15.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ares puesto 16.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ares puesto 17.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ares puesto 18.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Ares puesto 19.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Thanos puesto 20.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Thanos puesto 21.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Thanos puesto 22.", Company = "bravent" },
                    new WorkSpace { Name = "espacio Central, Thanos puesto 23.", Company = "bravent" }
                 );
                _context.SaveChanges();

            }
        }
    }
}
