using Microsoft.AspNetCore.Identity;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IAccountReponsitory
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
