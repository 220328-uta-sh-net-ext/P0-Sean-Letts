using UserInterface;
using User;
using Resturant;

Menus menu = new Menus();
string connectionStringFilePath = "C:/Users/Owner/Desktop/Revature/Sean-Letts/Project0_Sean_Letts/User/UserDatabase/SQLinfo.txt";
string connectionString = File.ReadAllText(connectionStringFilePath);
IUserLogic userinfo = new UserLogic(connectionString);
ResturantLogic resLogic = new ResturantLogic(connectionString);

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
            UserInfo newUser = menu.RegisterMenu();
            userinfo.addNewUser(newUser);
            //add new user to the json file
            //returns user to main menu after registering them
            break;
        case "NormalLogin":
        case "AdminLogin":
            //brings user to login menu
            UserInfo loginUser = menu.LoginMenu();
            if(input == "AdminLogin")
            {
                loginUser.IsAdmin = true;
            }
            bool isRealUser = userinfo.validateUser(loginUser);
            bool indrMenu = true;
            if (!isRealUser)
            {
                //if the data entered is wrong, return to menu screen
                indrMenu = false;
            }
            DirectionalMenu drMenu = new DirectionalMenu();
            if(loginUser.IsAdmin = true && isRealUser)
            {
                bool inAdminMenu = true;
                while (inAdminMenu)
                {
                    //Admin Menu
                    Console.WriteLine("Welcome to the admin menu.");
                    AdminMenu adMenu = new AdminMenu();
                    string adInput = adMenu.MainMenu();
                    switch (adInput)
                    {
                        case "FullExit":
                            inAdminMenu = false;
                            indrMenu = false;
                            repeat = false;
                            break;
                        case "Exit":
                            inAdminMenu = false;
                            indrMenu = false;
                            break;
                        case "NormalMenu":
                            inAdminMenu=false;
                            break;
                        case "ShowAll":
                            break;
                        case "Search":
                            break;
                        default:
                            Console.WriteLine("How the heck did you get here? Error.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
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
                    case "AddNew":
                        ResturantInfo newResturant = resLogic.getResturantInfo();
                        resLogic.addNewResturant(newResturant);
                        //Add a new resturant. Gonna need a lot of details.
                        break;
                    case "ViewAll":
                        var resturants = resLogic.GetAllResturants();
                        foreach (var resturant in resturants)
                            Console.WriteLine(resturant); 
                        Console.WriteLine("Press enter to exit");
                        Console.ReadLine();
                        break;
                    case "Search":
                        bool inReview = true;
                        while (inReview)
                        {
                            SearchMenu reviewMenu = new SearchMenu();
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
                                    resLogic.searchByName();
                                    break;
                                case "Address":
                                    resLogic.searchByAddress();
                                    break;
                                case "Zip Code":
                                    resLogic.searchByZipCode();
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