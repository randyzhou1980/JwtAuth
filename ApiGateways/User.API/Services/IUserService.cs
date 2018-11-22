using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.API.Entities;

namespace User.API.Services
{
    public interface IUserService
    {
        Task<IUserDetails> AuthenticateAsync(string username, string password);
        Task<IEnumerable<IUserDetails>> GetAllAsync();
    }
}
