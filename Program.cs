using System;
using System.Collections.Generic;
using System.Threading;

namespace ATM_Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            //is there somebody at the ATM
            bool inUse = true;

            //value tracker for Login and Registration menu
            int MainMenu;

            //value to keep track if there is a user logged in
            int userLoggedIn = -1;
            
            //Captures information about the logged in user
            string sessionFname = "";
            string sessionLName = "";
            string sessionPass = "";

            //List of preconfigured Accounts objects for demonstration purposes
            List<Accounts> listOfAccounts = new List<Accounts>
            {
                new Accounts("Reginald", "Richardson", "rocky#Balboa12356"),
                new Accounts("Reginald", "Richardson", "rocky#Balboa1235")
            };

            //While ATM is accessed
            while (inUse)
            {
                //create an instance of ATM to access methods
                ATM session = new ATM();

                //Display the login/registration menu
                session.LoginRegisterMenu();

                //get input from user if they want to login or create a new account
                MainMenu = session.TaskOptSelectValidation(session.GetUserInput("Please  enter a number for one of the options above\n" ),1,3);
                
                //switch for picking registration or login operation
                switch (MainMenu)
                {
                    case 1:
                        //send list of Accounts to Register method to create a new Account
                        session.Register(listOfAccounts);
                        break;

                    case 2:
                        //captures if user was able to login with credentials provided.  If yes, returns the index of the Accounts object
                        //user gets 3 tries to enter a correct username and password combination.  If exceeded, returns -1 and won't allow
                        //the printing of the logged in user menu
                        userLoggedIn = session.Login(listOfAccounts);
                        if(userLoggedIn == -1)
                        {
                            
                        }
                        //get the information of the logged in user if applicable
                        sessionFname = listOfAccounts[userLoggedIn].FirstName;
                        sessionLName = listOfAccounts[userLoggedIn].LastName;
                        sessionPass = listOfAccounts[userLoggedIn].Password;
                        break;


                    case 3:
                        //option to exit out of the program entirely
                        inUse = false;
                        break;
                }

                //check to determine if a user has logged in successfully
                while (userLoggedIn >= 0)
                {
                    //creates an instance of the ATM object to access the ATM members
                    ATM loggedInSession = new ATM();

                    //Prints the menu for the logged in to access operations against their account
                    loggedInSession.LoggedinRegisterMenu();

                    //Gets the users selection for an operation to perform once they are logged in
                    string lUserInput = loggedInSession.GetUserInput($"\n{sessionFname}\nSelect option [1-4]: ");
                    
                    //Validates and converts the users input into an integer format
                    int userOption = loggedInSession.TaskOptSelectValidation(lUserInput, 1, 4);

                    //Menu options for a logged in User
                    switch (userOption)
                    {

                        //Check balance
                        case 1:
                            double usersBalance = loggedInSession.CheckBalance(listOfAccounts, userLoggedIn);
                            Console.WriteLine($"Your current balance is: ${usersBalance}");
                            string continueLoggedInOp = loggedInSession.GetUserInput("Would you like to perform another transaction [y/n]: ");
                            userLoggedIn = loggedInSession.Logout(listOfAccounts[userLoggedIn], continueLoggedInOp, userLoggedIn);
                            Console.Clear(); 
                            break;

                        //Withdraw
                        case 2:
                            string DuserInput = loggedInSession.GetUserInput($" How much do you want to deposit today {listOfAccounts[userLoggedIn].FirstName}: $");
                            double depositAmount = loggedInSession.ValidateDoubles(DuserInput);
                            loggedInSession.Deposit(listOfAccounts, depositAmount, userLoggedIn);
                            continueLoggedInOp = loggedInSession.GetUserInput("Would you like to perform another transaction [y/n]: ");
                            userLoggedIn = loggedInSession.Logout(listOfAccounts[userLoggedIn], continueLoggedInOp, userLoggedIn);
                            Console.Clear(); break;

                        //Deposit
                        case 3:
                            string WuserInput = loggedInSession.GetUserInput($" How much do you want to withdraw today {listOfAccounts[userLoggedIn].FirstName}: $");
                            double withDrawAmount = loggedInSession.ValidateDoubles(WuserInput);
                            loggedInSession.Withdraw(listOfAccounts, withDrawAmount, userLoggedIn);
                            continueLoggedInOp = loggedInSession.GetUserInput("Would you like to perform another transaction [y/n]: ");
                            userLoggedIn = loggedInSession.Logout(listOfAccounts[userLoggedIn], continueLoggedInOp, userLoggedIn);
                            Console.Clear(); 
                            break;

                        //Logout
                        case 4:
                            loggedInSession.Logout(listOfAccounts[userLoggedIn]);
                            Console.Clear();
                            //listOfAccounts[userLoggedIn].loggedInStatus = false;
                            userLoggedIn = -1;
                            break;
                    }

                }

            }
        }
        
    }
}
