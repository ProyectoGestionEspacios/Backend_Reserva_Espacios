using Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public DataBaseContext Context { get; }

        public UnitOfWork(DataBaseContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges(); // guarda los datos y manda a la base de datos
        }
        public void Dispose()
        {
            Context?.Dispose();//limpia los recursos que se han quedado en memoria
        }
    }
}
