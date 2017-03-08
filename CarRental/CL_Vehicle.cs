using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_Vehicle
    {
        //Attributes
        private int vehicleID;
        private int vehicleTypeID;
        private int InsurancePackageID;
        private String description;
        private String brand;
        private int vintage;
        private int mileage;
        private bool gearChange;
        private int seat;
        private int door;
        private bool navigation;
        private bool airConditioning;
        private double rentPerDay;     
        private bool availability;


        //Getter and Setter
        public int VehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }
        public int VehicleTypeID
        {
            get { return vehicleTypeID; }
            set { vehicleTypeID = value; }
        }
        public int InsurancePackageID1
        {
            get { return InsurancePackageID; }
            set { InsurancePackageID = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public int Vintage
        {
            get { return vintage; }
            set { vintage = value; }
        }
        public int Mileage
        {
            get { return mileage; }
            set { mileage = value; }
        }
        public bool GearChange
        {
            get { return gearChange; }
            set { gearChange = value; }
        }
        public int Seat
        {
            get { return seat; }
            set { seat = value; }
        }
        public int Door
        {
            get { return door; }
            set { door = value; }
        }
        public bool Navigation
        {
            get { return navigation; }
            set { navigation = value; }
        }
        public bool AirConditioning
        {
            get { return airConditioning; }
            set { airConditioning = value; }
        }
        public double RentPerDay
        {
            get { return rentPerDay; }
            set { rentPerDay = value; }
        }
        public bool Availability
        {
            get { return availability; }
            set { availability = value; }
        }
    }
}
