using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IAuthenticationServices
    {

        //Task<UserDto> LoginAsync(LoginDto loginDto);

        ////Register
        ////take username ,phone email, password, displayname || return token , email , displayname

        //Task<UserDto> RegisterAsync(RegisterDto registerDto);

        ////check email 
        ////take string email || return true or false
        //Task<bool> CheckEmailAsync(string email);


        //Task<UserDto> GetCurrentUserAsync(string email);

    }
}
