using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ATM_Lab11
{
    class ATM
    {
        public bool IsLoggedIn { get; set;}
        public List<string> CurrentUser { get; set; }

        public ATM()
        {

        }
        public ATM(bool isLoggedIn, List<string> currentUser)
        {
            isLoggedIn = isLoggedIn;
        }

        public  string AtmMachine()
        {
            return ("Welcome to Road to Riches Bank." +
                "======================================"+
                "Main Menu:"+
                "Please select an option below:\n\n" +
                "       1. Register a New Account\n       2. Login to a Current Account\n");
        }

        public string GetUserInput(string message)
        {
            string userInput;
            Console.WriteLine(message);
            userInput = Console.ReadLine();
            return userInput;
        }

        public  int TaskOptSelectValidation(string userInput, int option1, int optionMax)
        {
            int userOption;
            try
            {
                userOption = int.Parse(userInput);
                if (userOption >= option1 && userOption <= optionMax)
                {
                    return userOption;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine($"Invalid entry.  Valid options ({option1} - {optionMax})");
                    Thread.Sleep(3500);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return TaskOptSelectValidation(GetUserInput(AtmMachine()), option1, optionMax);
                }
            }

            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid entry.  Valid options ({option1} - {optionMax})");
                Thread.Sleep(3500);
                Console.ForegroundColor = ConsoleColor.White;
                return TaskOptSelectValidation(GetUserInput(AtmMachine()), option1, optionMax);
            }
        }

        public List<Accounts> Register(List<Accounts> inComingAccounts)
        {
            int originalNum = inComingAccounts.Count;
            string firstName;
            string lastName;
            string passWord;

            firstName = GetUserInput("Please enter in your first name: ");
            lastName  = GetUserInput("Please enter in your last name: ");
            passWord = GetUserInput("Please specify a password for you new account (i.e. first word must be a letter and password must contain at least 1 number and 1 special character");

            Accounts account = new Accounts(firstName, lastName, passWord);
            inComingAccounts.Add(account);

            if(inComingAccounts.Count <= originalNum)
            {
                Console.WriteLine("An internal issue occured when trying to create this new account.  Please see an administrator for assistance");
            }
            else
            {
                Console.WriteLine("New account added successfully!!");
            }
            return inComingAccounts;
        }

        public bool Login(List<Accounts> inComingAccounts)
        {
            int loginAttempts = 3;
            bool isLoggedIn = false;
            bool allowLogin = AllowLogin(inComingAccounts);
            string firstName;
            string lastName;

            if (allowLogin == true)
            {
                firstName = GetUserInput("Please enter your First name : ");
                lastName = GetUserInput("Please enter your Last name : ");
                string passWord = GetUserInput("Please enter in the Password for the account: ");

                foreach (Accounts userAccount in inComingAccounts)
                {
                    if (firstName == userAccount.FirstName && lastName == userAccount.LastName && passWord == userAccount.Password)
                    {
                        userAccount.loggedInStatus = true;
                        Console.WriteLine("Login was successful!");
                        return isLoggedIn = true;
                        break;
                    }

                    else if ((firstName != userAccount.FirstName || lastName != userAccount.LastName || passWord != userAccount.Password) && loginAttempts > 0)
                    {
                        loginAttempts--;
                        Console.WriteLine($"Login attempt was unsuccessful! {loginAttempts} remaining");
                        return Login(inComingAccounts);
                    }

                    else if (firstName != userAccount.FirstName || lastName != userAccount.LastName || passWord != userAccount.Password || loginAttempts == 0)
                    {
                        Console.WriteLine($"{loginAttempts} remaining.  Please see an Administrator to access your Account");
                        isLoggedIn = false;
                    }
                }
                return isLoggedIn;
            }
            else
            {
                return isLoggedIn;
            }
        }

        private bool AllowLogin(List<Accounts> currentUserAccounts)
        {
            bool allowLogin = false;

            foreach (Accounts userAccount in currentUserAccounts)
            {
                if (userAccount.loggedInStatus == true)
                {
                    Console.WriteLine("Cannot log you in. Another user is currently logged in.");
                    allowLogin = false;
                }
                else
                {
                    allowLogin = true;
                }
            }
            return allowLogin;
        }

        public double CheckBalance(List<Accounts> username)
        {

        }

        /*public List<Accounts> Deposit(List<Accounts>string username, bool isLoggedIn)
        {

        }

        public List<Accounts> WithDraw(List<Accounts> string username, bool isLoggedIn)
        {

        }

        public bool Logout()
        {

        }*/

    }
}
