using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.DAL.Entites;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork uow;
        public UserService(IUnitOfWork wrk)
        {
            uow = wrk;
        }
        public UserDTO Login(UserDTO user)
        {
            Func<User, bool> FindUser = d => d.Email == user.Email && d.Password == user.Password;
            var temp = uow.Users.Find(FindUser).First();
            if (temp!=null)
            {
                throw new ValidationException("Неправлильно введена почта или пароль", "");
            }
            return new UserDTO { Id = temp.Id, Email = temp.Email, FirstName = temp.FirstName, LastName=temp.LastName, Password= temp.Password,UserRole= new UserRoleDTO {Name=temp.UserRole.Name }  };
        }

        public void Register(UserDTO user)
        {
            Func<User, bool> FindUser = d => d.Email == user.Email;
            if (uow.Users.Find(FindUser).First()==null)
            {
                throw new ValidationException("Пользователь с введенной почтой уже зарегистрирован","");
            }
            uow.Users.Create(new User { Email=user.Email, FirstName=user.FirstName, LastName=user.LastName, Password=user.Password});
        }

        public IEnumerable<UserRoleDTO> GetRoles()
        {
            var temp = uow.Roles.GetAll();
            List<UserRoleDTO> roles = new List<UserRoleDTO>();
            foreach (var e in temp)
            {
                roles.Add(new UserRoleDTO {Id=e.Id,Name=e.Name });
            }
            return roles;
        }
        public void Dispose()
        {
            uow.Dispose();
        }
    }
}
