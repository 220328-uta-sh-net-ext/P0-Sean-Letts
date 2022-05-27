using Models;
using User;

namespace ResturantAPI.Repository
{
    public interface IJWTManagerRepo
    {
        Tokens Authenticate(UserInfo user);
    }
}
