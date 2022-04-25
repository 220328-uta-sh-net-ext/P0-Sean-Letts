using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface
{
    public interface IMenus
    {
        public string MainMenu();
        public string MainUserChoice();
        public User.UserDetails LoginMenu();
        public User.UserDetails RegisterMenu();
        public string ValidateInput(string detail, string input);
    }
}
