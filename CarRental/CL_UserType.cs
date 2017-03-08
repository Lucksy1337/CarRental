using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_UserType
    {
        //Attributes
        private int userTypeID;
        private String description;


        //Getter and Setter
        public int UserTypeID
        {
            get { return userTypeID; }
            set { userTypeID = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
