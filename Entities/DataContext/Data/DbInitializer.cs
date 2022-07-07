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
                    new WorkSpace { Name = "puesto1", Company = "bitwork" },
                    new WorkSpace { Name = "puesto2", Company = "bitwork" },
                    new WorkSpace { Name = "puesto3", Company = "bitwork" },
                    new WorkSpace { Name = "puesto4", Company = "bitwork" },

                    new WorkSpace { Name = "puesto5", Company = "marketing" },
                    new WorkSpace { Name = "puesto6", Company = "marketing" },
                    new WorkSpace { Name = "puesto7", Company = "marketing" },
                    new WorkSpace { Name = "puesto8", Company = "marketing" },
                    new WorkSpace { Name = "puesto9", Company = "marketing" },

                    new WorkSpace { Name = "puesto11", Company = "bravent" },
                    new WorkSpace { Name = "puesto12", Company = "bravent" },
                    new WorkSpace { Name = "puesto13", Company = "bravent" },
                    new WorkSpace { Name = "puesto14", Company = "bravent" },
                    new WorkSpace { Name = "puesto15", Company = "bravent" },
                    new WorkSpace { Name = "puesto16", Company = "bravent" },
                    new WorkSpace { Name = "puesto17", Company = "bravent" },
                    new WorkSpace { Name = "puesto18", Company = "bravent" },
                    new WorkSpace { Name = "puesto19", Company = "bravent" },
                    new WorkSpace { Name = "puesto20", Company = "bravent" },
                    new WorkSpace { Name = "puesto21", Company = "bravent" },
                    new WorkSpace { Name = "puesto22", Company = "bravent" },
                    new WorkSpace { Name = "puesto23", Company = "bravent" }
                 );
                _context.SaveChanges();

            }
        }
    }
}
