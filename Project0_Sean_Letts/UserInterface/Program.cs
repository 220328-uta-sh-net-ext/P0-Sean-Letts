using UserInterface;
Menus menu = new Menus();

bool repeat = true;
while (repeat)
{
    string input = menu.MainMenu();
    switch (input)
    {
        case "Exit":
            repeat = false;
            break;
        case "Register":
            User.UserDetails newUser = menu.RegisterMenu();
            newUser.addNewUser(newUser);
            //add new user to the json file
            break;
        case "NormalLogin":
        case "AdminLogin":
            User.UserDetails loginUser = menu.LoginMenu();
            if(input == "AdminLogin")
            {
                loginUser.IsAdmin = true;
            }
            bool isRealUser = loginUser.validateUser(loginUser);
            if (isRealUser)
                //continue on
                Console.WriteLine("Is a real user");
            else
                Console.WriteLine("Is a fake user");
            //check user logging in details are all valid
            bool inReviews = true;
            ReviewMenus reviewMenus = new ReviewMenus();
            while (inReviews)
            {
                string reviewInput = reviewMenus.searchResturantMenu();
                switch (reviewInput)
                {
                    case "Exit":
                        inReviews = false;
                        break;
                    case "Name":
                        break;
                    case "Rating":
                        break;
                    case "ZipCode":
                        break;
                }
    }
            break;
        default:
            Console.WriteLine("How did you get here?");
            break;
    }

}
Console.WriteLine("Thank you for using our service.");