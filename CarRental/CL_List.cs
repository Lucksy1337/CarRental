using CarRental.CarRentalServiceReference;
//using CarRental.CarRentalSchoolServiceReference;
//using CarRental.CarRentalEbertsonServiceReference;

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
        private ObservableCollection<Auftrag> orderSortedByCustomerList;


        //Comparison lists  
        private ObservableCollection<Adresse> addressComparisonList;
        private ObservableCollection<Anmeldung> loginComparisonList;
        private ObservableCollection<Auftrag> orderComparisonList;
        private ObservableCollection<Benutzer> userComparisonList;
        private ObservableCollection<Fahrzeug> vehicleComparisonList;
        private ObservableCollection<Kontakt> contactComparisonList;
        private ObservableCollection<Kunde> customerComparisonList;


        //Add lists       
        private List<Adresse> addressAddList;
        private List<Kontakt> contactAddList;
        private List<Kunde> customerAddList;
        private List<Fahrzeug> vehicleAddList;        
        private List<Auftrag> orderAddList;    
        private List<Anmeldung> loginAddList;    
        private List<Benutzer> userAddList;
    

        //Delete lists
        private List<Fahrzeug> vehicleDeleteList;
        private List<Auftrag> orderDeleteList;
        private List<Anmeldung> loginDeleteList;
        private List<Benutzer> userDeleteList;


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
        public ObservableCollection<Auftrag> OrderSortedByCustomerList
        {
            get { return orderSortedByCustomerList; }
            set { orderSortedByCustomerList = value; }
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

        //Getter and Setter Update/Delete lists
        public List<Adresse> AddressAddList
        {
            get { return addressAddList; }
            set { addressAddList = value; }
        }
        public List<Kontakt> ContactAddList
        {
            get { return contactAddList; }
            set { contactAddList = value; }
        }
        public List<Kunde> CustomerAddList
        {
            get { return customerAddList; }
            set { customerAddList = value; }
        }
        public List<Fahrzeug> VehicleAddList
        {
            get { return vehicleAddList; }
            set { vehicleAddList = value; }
        }
        public List<Auftrag> OrderAddList
        {
            get { return orderAddList; }
            set { orderAddList = value; }
        }
        public List<Anmeldung> LoginAddList
        {
            get { return loginAddList; }
            set { loginAddList = value; }
        }
        public List<Benutzer> UserAddList
        {
            get { return userAddList; }
            set { userAddList = value; }
        }
        public List<Fahrzeug> VehicleDeleteList
        {
            get { return vehicleDeleteList; }
            set { vehicleDeleteList = value; }
        }
        public List<Auftrag> OrderDeleteList
        {
            get { return orderDeleteList; }
            set { orderDeleteList = value; }
        }
        public List<Anmeldung> LoginDeleteList
        {
            get { return loginDeleteList; }
            set { loginDeleteList = value; }
        }
        public List<Benutzer> UserDeleteList
        {
            get { return userDeleteList; }
            set { userDeleteList = value; }
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
        public void addToVehicleSortedByTypeList(Fahrzeug aVehicle)
        {
            VehicleSortedByTypeList.Add(aVehicle);
        }      
        public void addToOrderSortedByCustomerList(Auftrag aOrder)
        {
            OrderSortedByCustomerList.Add(aOrder);
        }
    }
}