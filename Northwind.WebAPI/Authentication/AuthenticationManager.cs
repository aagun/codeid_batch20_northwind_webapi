using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Northwind.Contracts.Dto.AuthenticationWebAPI;

using System.Text;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using Northwind.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Northwind.Domain.Repositories;
using Northwind.Persistence.RepositoryContext;
using System.Data;
using Northwind.Domain.Base;

namespace Northwind.WebAPI.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private Users _user;
        private readonly IRepositoryManager _repositoryManager;

        public AuthenticationManager(IConfiguration configuration, IRepositoryManager repositoryManager)
        {
            _configuration = configuration;
            _repositoryManager = repositoryManager;
        }

        public async Task<string> CreateToken()
        {
            var signInCredential = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signInCredential, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return (Convert.ToBase64String(randomNumber));
            }
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            /*_user = await _userManager.FindByNameAsync(userForAuth.UserName);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));*/
            _user = _repositoryManager.UserRepository.FindUserByUsername(userForAuth.UserName);
           

            return (_user != null && _repositoryManager.UserRepository.SignIn(userForAuth.UserName, userForAuth.Password));
            
            //throw new NotImplementedException();
        }

        private SigningCredentials GetSigningCredentials()
        {
            //var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
            var key= Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
             new Claim(ClaimTypes.Name, _user.UserName)
             };

            var roles = new List<string> { "Manager"};//await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
            //throw new NotImplementedException();
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings.GetSection("validIssuer").Value,
                audience: jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    }
}
