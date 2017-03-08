using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_InsurancePackage
    {
        //Attributes
        private int insurancePackageID;
        private string description;
        private bool occupationalAccidentInsurance;
        private bool liabilityInsurance;
        private bool fireInsurance;
        private bool liabilityLimitation;
        private bool theftProtection;


        //Getter and Setter
        public int InsurancePackageID
        {
            get { return insurancePackageID; }
            set { insurancePackageID = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public bool OccupationalAccidentInsurance
        {
            get { return occupationalAccidentInsurance; }
            set { occupationalAccidentInsurance = value; }
        }
        public bool LiabilityInsurance
        {
            get { return liabilityInsurance; }
            set { liabilityInsurance = value; }
        }
        public bool FireInsurance
        {
            get { return fireInsurance; }
            set { fireInsurance = value; }
        }
        public bool LiabilityLimitation
        {
            get { return liabilityLimitation; }
            set { liabilityLimitation = value; }
        }
        public bool TheftProtection
        {
            get { return theftProtection; }
            set { theftProtection = value; }
        }
    }
}
