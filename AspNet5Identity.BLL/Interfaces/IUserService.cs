using AspNet5Identity.BLL.DTO;
using AspNet5Identity.BLL.Inftastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNet5Identity.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<UserShortDTO> GetUserShortByEmail(string email);
        Task<List<UserShortDTO>> GetUsersShort(string search = "", string sort = "");
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
