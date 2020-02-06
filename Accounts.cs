using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Lab11
{
    //accounts class
    class Accounts
    {
        //accounts properties
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }

        public bool loggedInStatus { get; set; }

        //constructor for Accounts objects
        public Accounts(string firstname, string lastname, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Password = password;
            this.loggedInStatus = false;
            this.Balance = 0;
        }
        
    }
}
