using CarRental.CarRentalServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CL_List
    {
        // Singleton
        private static volatile CL_List instance;
        private static object syncRoot = new Object();


        //Main lists
        private ObservableCollection<Adresse> addressList;
        private ObservableCollection<Anmeldung> loginList;
        private ObservableCollection<Auftrag> orderList;
        private ObservableCollection<Benutzer> userList;
        private ObservableCollection<Benutzerart> userTypeList;
        private ObservableCollection<Fahrzeug> vehicleList;
        private ObservableCollection<Fahrzeugtyp> vehicleTypeList;
        private ObservableCollection<Kontakt> contactList;
        private ObservableCollection<Kunde> customerList;
        private ObservableCollection<Versicherungspaket> insurancePackageList;
        private ObservableCollection<Fahrzeug> vehicleSortedByTypeList;


        //Comparison lists  
        private ObservableCollection<Adresse> addressComparisonList;
        private ObservableCollection<Anmeldung> loginComparisonList;
        private ObservableCollection<Auftrag> orderComparisonList;
        private ObservableCollection<Benutzer> userComparisonList;
        private ObservableCollection<Fahrzeug> vehicleComparisonList;
        private ObservableCollection<Kontakt> contactComparisonList;
        private ObservableCollection<Kunde> customerComparisonList;


        //Getter and Setter Main lists
        public ObservableCollection<Adresse> AddressList
        {
            get { return addressList; }
            set { addressList = value; }
        }
        public ObservableCollection<Anmeldung> LoginList
        {
            get { return loginList; }
            set { loginList = value; }
        }
        public ObservableCollection<Auftrag> OrderList
        {
            get { return orderList; }
            set { orderList = value; }
        }
        public ObservableCollection<Benutzer> UserList
        {
            get { return userList; }
            set { userList = value; }
        }
        public ObservableCollection<Benutzerart> UserTypeList
        {
            get { return userTypeList; }
            set { userTypeList = value; }
        }
        public ObservableCollection<Fahrzeug> VehicleList
        {
            get { return vehicleList; }
            set { vehicleList = value; }
        }
        public ObservableCollection<Fahrzeugtyp> VehicleTypeList
        {
            get { return vehicleTypeList; }
            set { vehicleTypeList = value; }
        }
        public ObservableCollection<Kontakt> ContactList
        {
            get { return contactList; }
            set { contactList = value; }
        }
        public ObservableCollection<Kunde> CustomerList
        {
            get { return customerList; }
            set { customerList = value; }
        }
        public ObservableCollection<Versicherungspaket> InsurancePackageList
        {
            get { return insurancePackageList; }
            set { insurancePackageList = value; }
        }
        public ObservableCollection<Fahrzeug> VehicleSortedByTypeList
        {
            get { return vehicleSortedByTypeList; }
            set { vehicleSortedByTypeList = value; }
        }


        //Getter and Setter Comparison lists     
        public ObservableCollection<Adresse> AddressComparisonList
        {
            get { return addressComparisonList; }
            set { addressComparisonList = value; }
        }
        public ObservableCollection<Anmeldung> LoginComparisonList
        {
            get { return loginComparisonList; }
            set { loginComparisonList = value; }
        }
        public ObservableCollection<Auftrag> OrderComparisonList
        {
            get { return orderComparisonList; }
            set { orderComparisonList = value; }
        }
        public ObservableCollection<Benutzer> UserComparisonList
        {
            get { return userComparisonList; }
            set { userComparisonList = value; }
        }
        public ObservableCollection<Fahrzeug> VehicleComparisonList
        {
            get { return vehicleComparisonList; }
            set { vehicleComparisonList = value; }
        }
        public ObservableCollection<Kontakt> ContactComparisonList
        {
            get { return contactComparisonList; }
            set { contactComparisonList = value; }
        }
        public ObservableCollection<Kunde> CustomerComparisonList
        {
            get { return customerComparisonList; }
            set { customerComparisonList = value; }
        }


        //Instantiation of CL_List
        public static CL_List Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new CL_List();
                    }
                }
                return instance;
            }
        }


        //Methods
        public void addToAddressList(Adresse newAddress)
        {
            addressList.Add(newAddress);
        }
        public void addToLoginList(Anmeldung newLogin)
        {
            loginList.Add(newLogin);
        }
        public void addToOrderList(Auftrag newOrder)
        {
            orderList.Add(newOrder);
        }
        public void addToUserList(Benutzer newUser)
        {
            userList.Add(newUser);
        }
        public void addToVehicleList(Fahrzeug newVehicle)
        {
            vehicleList.Add(newVehicle);
        }      
        public void addToContactList(Kontakt newContact)
        {
            contactList.Add(newContact);
        }
        public void addToCustomerList(Kunde newCustomer)
        {
            customerList.Add(newCustomer);
        }      
    }
}