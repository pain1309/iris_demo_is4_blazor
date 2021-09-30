using AuthServer.Shared.Models;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.Refactor.Services
{
    public interface IAuthService
    {
        Task Login(LoginRequest loginRequest);
        Task Register(RegisterRequest registerRequest);
        Task Logout();
        Task<CurrentUser> CurrentUserInfo();
    }
}
