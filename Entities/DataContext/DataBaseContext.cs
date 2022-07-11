using Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataContext
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser>
    {
       public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
       {
       }

        // dotnet ef migrations add Authentication. para pasar la cadena de migration en la consola dotnet ef migrations add Authentication, ejecutar y luego activar el constructor anterior
        //luego dotnet ef database update actualizamos la base de datos

        //protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        //{
        //   dbContextOptionsBuilder.UseSqlServer("Data Source=DOMIRUS\\SQLEXPRESS;Initial Catalog=GestionReservasDB;Integrated Security=True");
        //}


        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WorkSpace> WorkSpaces { get; set; }
        public DbSet<Meeting> Meeting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>()
                     .HasOne(x => x.WorkSpace);

            modelBuilder.Entity<WorkSpace>()
                    .HasMany(x => x.Reservations);



        }
    }
}
