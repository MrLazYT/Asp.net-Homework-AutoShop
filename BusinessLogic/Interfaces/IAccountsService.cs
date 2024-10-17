using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTOs.User;

namespace BusinessLogic.Interfaces
{
    public interface IAccountsService
    {
        Task<IdentityUser> Get(string id);
        Task<string> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
        Task Logout();
    }
}
