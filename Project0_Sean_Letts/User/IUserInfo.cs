using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public interface IUserInfo
    {
        void addNewUser(UserDetails newuser);
        bool validateUser(UserDetails loginUser);
    }
}
