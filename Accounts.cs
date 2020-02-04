using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Lab11
{
    class Accounts
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public int Balance { get; set; }

        public bool loggedInStatus { get; set; }
        public Accounts(string firstname, string lastname, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Password = password;
            this.loggedInStatus = false;
        }

        public Accounts()
        {

        }
        
    }
}
