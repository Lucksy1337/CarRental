using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_User
    {
        //Attributes
        private int userID;
        private int loginID;
        private int userTypeID;
        private String username;


        //Setter and Getter
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public int LoginID
        {
            get { return loginID; }
            set { loginID = value; }
        }
        public int UserTypeID
        {
            get { return userTypeID; }
            set { userTypeID = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
    }
}
