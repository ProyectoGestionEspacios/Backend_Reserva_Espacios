using Microsoft.AspNetCore.Identity;


namespace DataAccess.Repos
{
    public interface IUserRepository
    {
        Task<IdentityResult> Edit(string id, ApplicationUserDTO applicationUserDTO);

        bool ApplicationUserExists(string id);

        Task<IdentityResult> DeleteConfirmed(string id);

    }
}
