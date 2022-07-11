using Entities.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

    //private readonly SignInManager<ApplicationUser> _signInManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }
    

        public async Task<IdentityResult> Edit(string id, ApplicationUserDTO applicationUserDTO)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            usuario.Email = applicationUserDTO.Email;           
            usuario.Name = applicationUserDTO.Name;
            usuario.Surname = applicationUserDTO.Surname;
            usuario.UserName = applicationUserDTO.UserName;


            return await _userManager.UpdateAsync(usuario);

        }

        public bool ApplicationUserExists(string id)
        {
            return _userManager.Users.Any(u => u.Id == id);
        }

        public async Task<IdentityResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
            return await _userManager.DeleteAsync(applicationUser);
        }
    }
}
