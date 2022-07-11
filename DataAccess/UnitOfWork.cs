using DataAccess.Repos;
using Entities.Auth;
using Entities.DataContext;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;


        public UnitOfWork(DataBaseContext context, UserManager<ApplicationUser> userManager)
        {
            Context = context;
            _userManager = userManager;
        }

        public IUserRepository UserRepository => new UserRepository(_userManager);


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
