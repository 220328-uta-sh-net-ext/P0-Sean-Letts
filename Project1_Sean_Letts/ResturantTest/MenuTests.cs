using Xunit;
using User;
using UserInterface;
using System;
using System.IO;

namespace Project0Tests
{
    public class MenuTests
    {
        [Fact]
        public void AdminMenuTestZero()
        {
            IMenus adminMenu = new AdminMenu();
            var stringReader = new StringReader("0");
            Console.SetIn(stringReader);
            string adminAnswer = adminMenu.MainUserChoice();
            Assert.Equal(adminAnswer, "FullExit");
        }
        [Fact]
        public void AdminMenuTestOne()
        {
            IMenus adminMenu = new AdminMenu();
            var stringReader = new StringReader("1");
            Console.SetIn(stringReader);
            string adminAnswer = adminMenu.MainUserChoice();
            Assert.Equal(adminAnswer, "Exit");
        }
        [Fact]
        public void AdminMenuTestTwo()
        {
            IMenus adminMenu = new AdminMenu();
            var stringReader = new StringReader("2");
            Console.SetIn(stringReader);
            string adminAnswer = adminMenu.MainUserChoice();
            Assert.Equal(adminAnswer, "NormalMenu");
        }
        [Fact]
        public void AdminMenuTestThree()
        {
            IMenus adminMenu = new AdminMenu();
            var stringReader = new StringReader("3");
            Console.SetIn(stringReader);
            string adminAnswer = adminMenu.MainUserChoice();
            Assert.Equal(adminAnswer, "ShowAll");
        }
        [Fact]
        public void AdminMenuTestFour()
        {
            IMenus adminMenu = new AdminMenu();
            var stringReader = new StringReader("4");
            Console.SetIn(stringReader);
            string adminAnswer = adminMenu.MainUserChoice();
            Assert.Equal(adminAnswer, "Search");
        }
        [Fact]
        public void DirMenuTestZero()
        {
            IMenus dirMenu = new DirectionalMenu();
            var stringReader = new StringReader("0");
            Console.SetIn(stringReader);
            string dirAnswer = dirMenu.MainUserChoice();
            Assert.Equal(dirAnswer, "FullExit");
        }
        [Fact]
        public void SearchMenuTestZero()
        {
            IMenus searchMenu = new SearchMenu();
            var stringReader = new StringReader("0");
            Console.SetIn(stringReader);
            string searchAnswer = searchMenu.MainUserChoice();
            Assert.Equal(searchAnswer, "FullExit");
        }
        [Fact]
        public void MenusTestZero()
        {
            IMenus menu = new Menus();
            var stringReader = new StringReader("0");
            Console.SetIn(stringReader);
            string menuAnswer = menu.MainUserChoice();
            Assert.Equal(menuAnswer, "Exit");
        }
        /*
        [Fact]
        public void LoginMenuTest()
        {
            Menus menu = new Menus();
            UserInfo temp = new UserInfo();
            var username = new StringReader("USER_NAME\n");
            Console.SetIn(username);
            var password = new StringReader("PASS_WORD\n");
            Console.SetIn(password);
            temp = menu.LoginMenu();
            Assert.Equal("USER_NAME", temp.UserName);
            Assert.Equal("PASS_WORD", temp.Password);
        }*/
    }
}
