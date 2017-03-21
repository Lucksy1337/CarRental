using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Order
    {
        //Attributes
        private int orderID;
        private int customerID;
        private int vehicleID;        
        private DateTime orderDate;
        private DateTime returnDate;
        private double totalPrice;


        //Getter and Setter
        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public int VehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }       
        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        public DateTime ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; }
        }
        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }       
    }
}
