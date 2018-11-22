using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Entities
{
    public interface IUserDetails
    {
        int Id { get; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; }

        string Token { get; }
    }
}
