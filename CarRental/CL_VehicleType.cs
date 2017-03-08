using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_VehicleType
    {
        //Attributes
        private int vehicleTypeID;
        private String description;


        //Getter and Setter
        public int VehicleTypeID
        {
            get { return vehicleTypeID; }
            set { vehicleTypeID = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
