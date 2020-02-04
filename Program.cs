using System;
using System.Collections.Generic;

namespace ATM_Lab11
{
    class Program
    {
        static void Main(string[] args)
        {
            bool inUse = true;
            int MainMenu;
            bool userLoggedIn = false;
            List<Accounts> listOfAccounts = new List<Accounts>
            {
                new Accounts("Reginald", "Richardson", "rocky#Balboa1235")
            };

            while (inUse)
            {
                ATM session = new ATM();
                session.AtmMachine();
                MainMenu = session.TaskOptSelectValidation(session.GetUserInput(session.AtmMachine()), 1, 2);
                switch (MainMenu)
                {
                    case 1:
                        session.Register(listOfAccounts);
                        break;
                    case 2:
                        userLoggedIn = session.Login(listOfAccounts);
                        break;
                }
                while (userLoggedIn)
                {

                }

            }
        }
        
    }
}
