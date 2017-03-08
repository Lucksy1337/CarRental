using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Login
    {
        //Attributes
        private int loginID;
        private String username;
        private String password;


        //Getter and Setter
        public int LoginID
        {
            get { return loginID; }
            set { loginID = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
