using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessUsers
{
    public interface IUsersRepository
    {
        List<UserInfoME> GetUsers();

        UserInfoME GetUserById(int id);

        int CreateUser(UserInfoME usuarios);

        UserInfoME ValidateUser(UserInfoME usuario);

        int ModifyUser(UserInfoME usuarios);

        int DeleteUser(int id);

    }
}
