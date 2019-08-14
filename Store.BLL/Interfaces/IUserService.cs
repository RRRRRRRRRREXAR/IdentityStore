using Store.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO Login(UserDTO user);
        void Register(UserDTO user);
        IEnumerable<UserRoleDTO> GetRoles();
        void Dispose();
    }
}
