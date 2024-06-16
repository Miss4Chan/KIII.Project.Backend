using Domain.DTO;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<AuthenticatedUserDTO> SignUp(ShopApplicationUser user);
        Task<AuthenticatedUserDTO> SignIn(ShopApplicationUser user);
        UserDTO GetProfile(string username);
    }
}
