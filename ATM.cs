using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ATM_Lab11
{   
    //ATM class
    class ATM
    {
        //gets set when a user logs in(i.e and is checked in future login attempts to make sure another user is not 
        //logged in concurrently
        public bool IsLoggedIn { get; set;}

        //Property that store
        public ATM(bool isLoggedIn)
        {
            IsLoggedIn = isLoggedIn;
        }

        public ATM()
        {

        }
        
        //method that constructs and prints the registration/login page
        public void LoginRegisterMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;

            //login layout
            Console.WriteLine("======================================\n   Welcome to Road to Riches Bank\n" +
                "======================================\n\n"+
                "Main Menu\n\n"+
                "  1. Register a New Account\n  2. Login to a Current Account\n  3. Exit the Program");
        }

        //method that contructs and prints the page for login users
        public void LoggedinRegisterMenu()
        {
            Console.WriteLine($"============\nUSER MAIN MENU\n============\n\n    Would you like to:\n\n1. Check your balance\n2. Make a Deposit\n3. Make a Withdrawal\n4. Exit");
        }

        //we all know and love this one
        public string GetUserInput(string message)
        {
            string userInput;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);


            userInput = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            return userInput;
        }

        //validates all numeric inputs collected from the user
        //Makes sure inputs are numeric (i.e. can be parsed), and that they are number choices that fit acceptable ranges
        public  int TaskOptSelectValidation(string userInput, int option1, int optionMax)
        {
            int userOption;
            
            //try to parse to an int
            try
            {
                userOption = int.Parse(userInput);
                
                //check if number is between alloted range
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
                    
                    //recursion if number is not within the allotted range    
                    return TaskOptSelectValidation(GetUserInput("Invalid Option.  Please try again"), option1, optionMax);
                }
            }

            //if input cannot be parsed as an integer
            catch (FormatException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(3500);
                Console.ForegroundColor = ConsoleColor.White;
              
                //recursion until acceptable number is entered
                return TaskOptSelectValidation(GetUserInput("Invalid option. Please try again"), option1, optionMax);
            }
        }

        //Validates double datatype entries when entered by the user for withdraw and deposit methods
        public double ValidateDoubles(string value)
        {

            //try to parse user input as a double here
            try
            {
                double deposit = double.Parse(value);
                return deposit;
            }

            //throw exception when parse fails
            catch (FormatException)
            {

                //and what is this?? You freaking guessed it man!!! Recursion!! 
                return ValidateDoubles(GetUserInput("Invalid value entered.  Please enter in the amount you would like to deposit (i.e. 20.50): "));

            }
        }

        //Method to register a new user account
        public List<Accounts> Register(List<Accounts> inComingAccounts)
        {
            //captures the number of accounts in the Accounts list
            int originalNum = inComingAccounts.Count;
            string firstName;
            string lastName;
            string passWord;

            firstName = GetUserInput("Please enter in your first name: ");
            lastName  = GetUserInput("Please enter in your last name: ");
            passWord = GetUserInput("Please specify a password for you new account (i.e. first word must be a letter and password must contain at least 1 number and 1 special character");

            //Constructs a new Account
            Accounts account = new Accounts(firstName, lastName, passWord);
            inComingAccounts.Add(account);

            //check if new account has been added to the list
            if(inComingAccounts.Count <= originalNum)
            {
                Console.WriteLine("An internal issue occured when trying to create this new account.  Please see an administrator for assistance");
            }


            else
            {
                Console.WriteLine("New account added successfully!!");
            }

            //returns the account list with the new account
            return inComingAccounts;
        }

        //method to login with an existing account
        public int Login(List<Accounts> inComingAccounts)
        {
            //number of attempts user gets to login with valid credentials
            int loginAttempts = 3;

            //checks if there are any other accounts that are still marked as logged in
            bool allowLogin = AllowLogin(inComingAccounts);
            string firstName;
            string lastName;
            int i = 0;

            //if no other accounts logged in
            if (allowLogin == true)
            {
                Console.Clear();

                //get existing account information from the user
                firstName = GetUserInput("Please enter your First name : ");
                lastName = GetUserInput("Please enter your Last name : ");
                string passWord = GetUserInput("Please enter in the Password for the account: ");

                //while loop to track number of login attempts
                while (loginAttempts > 0)
                {
                    //loops through list of accounts to find the account matching the users input
                    foreach (Accounts userAccount in inComingAccounts)
                    {
                        //if account found in the existing list break out of the loop, set the found account as logged in
                        if (firstName == userAccount.FirstName && lastName == userAccount.LastName && passWord == userAccount.Password)
                        {
                            userAccount.loggedInStatus = true;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            //If you make it here you can make it anywhere!!  Lets the user know the login attempt was successful
                            Console.WriteLine("Login was successful!!");
                            Thread.Sleep(2000);
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            //returns the index of the account found in the list
                            return i;
                        }
                        //increments i if the account doesn't match before moving on to the next object in the list
                        i++;

                    }
                    loginAttempts--;

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    
                    
                    Console.WriteLine($"Invalid login.  Please make sure your username and password are correct\nYou have {loginAttempts} attempts remaining\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(3000);
                    
                    //returns the account object list
                    return Login(inComingAccounts);
                }
            }

            //deal with another account still marked as logged in
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not log in as another session is running.  Please try again later");
                Thread.Sleep(3500);
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(-1);
                return -1;
            }

            //exits if number of input trys is exceeded
            Environment.Exit(-1);
            return 0;
        }

        //method to check if an account is marked as logged in
        private bool AllowLogin(List<Accounts> currentUserAccounts)
        {
            bool allowLogin = false;

            //loops through accounts to find if one is marked as logged in
            foreach (Accounts userAccount in currentUserAccounts)
            {

                //doesn't allow login of another account if one is found as logged in
                if (userAccount.loggedInStatus == true)
                {
                    Console.WriteLine("Cannot log you in. Another user is currently logged in.");
                    allowLogin = false;
                }

                //specifies that another account can be used to login
                else
                {
                    allowLogin = true;
                }
            }


            return allowLogin;
        }

        //method to check the balance of target account
        public double CheckBalance(List<Accounts> accounts, int x)
        {
            return accounts[x].Balance;
        }

        //method to add money to the logged in account
        public List<Accounts> Deposit(List<Accounts> accounts, double depositAmount, int x)
        {
            double initialB = accounts[x].Balance;
            accounts[x].Balance = accounts[x].Balance + depositAmount;


            Console.Clear();
            Console.WriteLine($"{accounts[x].FirstName}, \n");
            Console.WriteLine($"Your Initial Balance: ${initialB}\nYour Deposit Amount: ${depositAmount}\nYour New Balance: ${accounts[x].Balance} ");
            
            return accounts;
        }

        //method to withdraw money from the logged in account
        public List<Accounts> Withdraw(List<Accounts> accounts, double withDrawAmount, int x)
        {
            double initialB = accounts[x].Balance;

            //doesn't allow withdraw if the amount being withdrawn puts the account at -$10
            if (initialB - withDrawAmount > -10)
            {
                accounts[x].Balance = accounts[x].Balance - withDrawAmount;
                Console.Clear();

                Console.WriteLine($"{accounts[x].FirstName}, \n");
                Console.WriteLine($"Your Initial Balance: ${initialB}\nYour Deposit Amount: ${withDrawAmount}\nYour New Balance: ${accounts[x].Balance} ");
                
                
                if (accounts[x].Balance < 0)
                {
                    Console.WriteLine($"You have a negative balance of ${accounts[x].Balance} after this transaction.\nMake sure you make a deposit before 12am tomorrow so that you don't incur and overdraft fee.");
                }

                return accounts;
            }

            //doesn't allow funds to be withdrawn
            else
            {
                Console.WriteLine($"{accounts[x].FirstName}, \n");
                Console.WriteLine("Insufficient Funds.");
                Console.WriteLine($"Your Initial Balance: ${initialB}\nYour Deposit Amount: ${withDrawAmount}\nYour New Balance: ${accounts[x].Balance} ");
                return accounts;
            }
        }

        //method to logout of an account
        public int Logout(Accounts accountObject, string userInput, int accountNum)
        {
            if (userInput != "y" && userInput != "n")
            {
                return Logout(accountObject, GetUserInput("Invalid option selected.  Please enter either y or n: "), accountNum);
            }

            else if (userInput == "y")
            {
                return accountNum;
            }

            else
            {
                accountObject.loggedInStatus = false;
                return -1;
            }
        }

        public int Logout(Accounts accountObject)
        {
            accountObject.loggedInStatus = false;
            return -1;
        }
    }
}
