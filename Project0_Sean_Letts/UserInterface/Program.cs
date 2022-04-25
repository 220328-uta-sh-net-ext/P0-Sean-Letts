using UserInterface;
Menus menu = new Menus();

bool repeat = true;
while (repeat)
{
    string input = menu.MainMenu();
    switch (input)
    {
        case "Exit": //exits the program
            repeat = false;
            break;
        case "Register": //brings user to register screen
            User.UserDetails newUser = menu.RegisterMenu();
            newUser.addNewUser(newUser);
            //add new user to the json file
            //returns user to main menu after registering them
            break;
        case "NormalLogin":
        case "AdminLogin":
            //brings user to login menu
            User.UserDetails loginUser = menu.LoginMenu();
            if(input == "AdminLogin")
            {
                loginUser.IsAdmin = true;
            }
            bool isRealUser = loginUser.validateUser(loginUser);
            bool indrMenu = true;
            if (!isRealUser)
            {
                //if the data entered is wrong, return to menu screen
                indrMenu = false;
            }
            DirectionalMenu drMenu = new DirectionalMenu();
            while (indrMenu)
            {
                string drInput = drMenu.MainMenu();
                switch (drInput)
                {
                    case "FullExit":
                        indrMenu = false;
                        repeat = false;
                        break;
                    case "Exit":
                        indrMenu = false;
                        break;
                    case "ViewAll":
                        bool inReview = true;
                        while (inReview)
                        {
                            ReviewMenus reviewMenu = new ReviewMenus();
                            string reviewInput = reviewMenu.MainMenu();
                            switch (reviewInput)
                            {
                                case "FullExit":
                                    inReview = false;
                                    indrMenu = false;
                                    repeat = false;
                                    break;
                                case "Exit":
                                    inReview = false;
                                    break;
                                case "Name":
                                case "Rating":
                                case "Zip Code":
                                    Console.WriteLine($"Pleast enter the {reviewInput} you'd like to search by.");
                                    string searchBy = Console.ReadLine();
                                    //Todo, logic for searching
                                    break;
                                default:
                                    Console.WriteLine("How did you get here? Error x2");
                                    break;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("How did you get here? Error");
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