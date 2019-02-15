using LoginTokenAPI.Models;
using LoginTokenAPI.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LoginTokenAPI.EFCore;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace LoginTokenAPI.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct);
    }

    public class UserService : IUserService
    {

        //https://blog.codingmilitia.com/2019/01/16/aspnet-011-from-zero-to-overkill-data-access-with-entity-framework-core

        private CadastrosContext _context;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, CadastrosContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        private User Login(string login, string password)
        {
            return _context.User.Where(u => u.Login.Equals(login) && u.Senha.Equals(password)).FirstOrDefault();
        }

        public string Authenticate(string username, string password)
        {
            var user = Login(username, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.CodigoUsuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var ret = tokenHandler.WriteToken(token);

            return ret;
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct)
        {
            var users = await _context.User.AsNoTracking().OrderBy(g => g.Nome).ToListAsync(ct);
            return users.Select(x => {
                x.Senha = null;
                return x;
            });
        }

    }
}
