using DataAccess.Repos;
using Entities.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public interface IUnitOfWork
    {
        DataBaseContext Context { get; }

        IUserRepository UserRepository { get; }

        void Commit();//guardar todo lo que hay en memoria manda a la base de datos

    }
}
