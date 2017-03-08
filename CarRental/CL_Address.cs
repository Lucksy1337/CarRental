using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Address
    {
        //Attributes
        private int addressID;
        private String street;
        private String houseNumber;
        private String zipCode;
        private String city;


        //Getter and Setter
        public int AddressID
        {
            get { return addressID; }
            set { addressID = value; }
        }
        public string Street
        {
            get { return street; }
            set { street = value; }
        }
        public string HouseNumber
        {
            get { return houseNumber; }
            set { houseNumber = value; }
        }
        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
       //TEXTXZ
    }
}
