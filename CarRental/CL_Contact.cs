using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Contact
    {
        //Attributes
        private int contactID;
        private String mail;
        private String phoneNumber;
        private String mobileNumber;
        private String faxNumber;


        //Getter and Setter
        public int ContactID
        {
            get { return contactID; }
            set { contactID = value; }
        }
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }
        public string FaxNumber
        {
            get { return faxNumber; }
            set { faxNumber = value; }
        }
    }
}
