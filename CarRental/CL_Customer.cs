using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Customer
    {
        //Attributes
        private int customerID;
        private int contactID;
        private int AddressID;
        private string firstName;
        private string lastName;
        private int age;
        private string gender;
        private string customerNumber;


        //Getter and Setter
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public int ContactID
        {
            get { return contactID; }
            set { contactID = value; }
        }
        public int AddressID1
        {
            get { return AddressID; }
            set { AddressID = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public string CustomerNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }
    }
}
