using Northwind.Contracts.Dto;
using Northwind.Contracts.Dto.AuthenticationWebAPI;
using System.Threading.Tasks;

namespace Northwind.WebAPI.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();

        string GenerateRefreshToken();

    }
}
