using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.CarRentalServiceReference;
using System.Data.Services.Client;

namespace CarRental
{
    class DM_DBConnection : DM_IDBConnection
    {
        //Singleton
        private static volatile DM_DBConnection instance;
        private static object syncRoot = new Object();


        //Entitie
        peterson_infrastrukturDBEntities _carRentalEntities;


        //Lists
        CL_List aList;
        IEnumerable<Adresse> addressListEnumerable;
        IEnumerable<Anmeldung> loginListEnumerable;
        IEnumerable<Auftrag> orderListEnumerable;
        IEnumerable<Benutzer> userListEnumerable;
        IEnumerable<Benutzerart> userTypeListEnumerable;
        IEnumerable<Fahrzeug> vehicleListEnumerable;
        IEnumerable<Fahrzeugtyp> vehicleTypeListEnumerable;
        IEnumerable<Kontakt> contactListEnumerable;
        IEnumerable<Kunde> customerListEnumerable;
        IEnumerable<Versicherungspaket> insurancePackageListEnumerable;


        //Constructor
        public DM_DBConnection()
        {
            aList = CL_List.Instance;
            _carRentalEntities = new peterson_infrastrukturDBEntities(
            new Uri("http://localhost:9938/WcfCarRentalDataService.svc"));

            //School
            //new Uri("http://localhost:5202/WcfCarRentalSchoolDataService.svc"));

            DataServiceQuery<Adresse> queryAddress = _carRentalEntities.Adresse;
            addressListEnumerable = queryAddress.Execute();

            DataServiceQuery<Kontakt> queryContact = _carRentalEntities.Kontakt;
            contactListEnumerable = queryContact.Execute();

            DataServiceQuery<Anmeldung> queryLogin = _carRentalEntities.Anmeldung;
            loginListEnumerable = queryLogin.Execute();

            DataServiceQuery<Benutzerart> queryUserType = _carRentalEntities.Benutzerart;
            userTypeListEnumerable = queryUserType.Execute();

            DataServiceQuery<Benutzer> queryUser = _carRentalEntities.Benutzer;
            userListEnumerable = queryUser.Execute();

            DataServiceQuery<Fahrzeugtyp> queryVehicleType = _carRentalEntities.Fahrzeugtyp;
            vehicleTypeListEnumerable = queryVehicleType.Execute();

            DataServiceQuery<Versicherungspaket> queryInsurancePackage = _carRentalEntities.Versicherungspaket;
            insurancePackageListEnumerable = queryInsurancePackage.Execute();

            DataServiceQuery<Fahrzeug> queryVehicle = _carRentalEntities.Fahrzeug;
            vehicleListEnumerable = queryVehicle.Execute();

            DataServiceQuery<Kunde> queryCustomer = _carRentalEntities.Kunde;
            customerListEnumerable = queryCustomer.Execute();

            DataServiceQuery<Auftrag> queryOrder = _carRentalEntities.Auftrag;
            orderListEnumerable = queryOrder.Execute();           

            aList.AddressList = new ObservableCollection<Adresse>(addressListEnumerable.ToList());
            aList.AddressComparisonList = new ObservableCollection<Adresse>(aList.AddressList.ToList());

            aList.ContactList = new ObservableCollection<Kontakt>(contactListEnumerable.ToList());
            aList.ContactComparisonList = new ObservableCollection<Kontakt>(aList.ContactList.ToList());

            aList.LoginList = new ObservableCollection<Anmeldung>(loginListEnumerable.ToList());
            aList.LoginComparisonList = new ObservableCollection<Anmeldung>(aList.LoginList.ToList());

            aList.UserTypeList = new ObservableCollection<Benutzerart>(userTypeListEnumerable.ToList());

            aList.UserList = new ObservableCollection<Benutzer>(userListEnumerable.ToList());
            aList.UserComparisonList = new ObservableCollection<Benutzer>(aList.UserList.ToList());                 

            aList.VehicleTypeList = new ObservableCollection<Fahrzeugtyp>(vehicleTypeListEnumerable.ToList());
            aList.InsurancePackageList = new ObservableCollection<Versicherungspaket>(insurancePackageListEnumerable.ToList());

            aList.VehicleList = new ObservableCollection<Fahrzeug>(vehicleListEnumerable.ToList());
            foreach (Fahrzeug aVehicle in aList.VehicleList)
            {
                foreach (Fahrzeugtyp aVehicleType in aList.VehicleTypeList)
                {
                    if (aVehicle.FahrzeugtypID == aVehicleType.FahrzeugtypID)
                    {
                        aVehicle.Fahrzeugtyp = aVehicleType;
                    }
                }

                foreach (Versicherungspaket aInsurancePackage in aList.InsurancePackageList)
                {
                    if (aVehicle.VersicherungspaketID == aInsurancePackage.VersicherungspaketID)
                    {
                        aVehicle.Versicherungspaket = aInsurancePackage;
                    }
                }
            }
            aList.VehicleComparisonList = new ObservableCollection<Fahrzeug>(aList.VehicleList.ToList());
            
            aList.CustomerList = new ObservableCollection<Kunde>(customerListEnumerable.ToList());
            foreach(Kunde aCustomer in aList.CustomerList)
            {
                foreach (Adresse aAddress in aList.AddressList)
                {
                    if (aCustomer.AdresseID == aAddress.AdresseID)
                    {
                        aCustomer.Adresse = aAddress;                       
                    }
                }

                foreach(Kontakt aContact in aList.ContactList)
                {
                    if(aCustomer.KontaktID == aContact.KontaktID)
                    {
                        aCustomer.Kontakt = aContact;
                    }
                }
            }              
            aList.CustomerComparisonList = new ObservableCollection<Kunde>(aList.CustomerList.ToList());

            aList.OrderList = new ObservableCollection<Auftrag>(orderListEnumerable.ToList());
            foreach (Auftrag aOrder in aList.OrderList)
            {
                foreach (Kunde aCustomer in aList.CustomerList)
                {
                    if (aOrder.KundeID == aCustomer.KundeID)
                    {
                        aOrder.Kunde = aCustomer;
                    }
                }

                foreach (Fahrzeug aVehicle in aList.VehicleList)
                {
                    if (aOrder.FahrzeugID == aVehicle.FahrzeugID)
                    {
                        aOrder.Fahrzeug = aVehicle;
                    }
                }
            }
            aList.OrderComparisonList = new ObservableCollection<Auftrag>(aList.OrderList.ToList());
            aList.VehicleSortedByTypeList = new ObservableCollection<Fahrzeug>();
            aList.VehicleSortedByOrderList = new ObservableCollection<Fahrzeug>();
            aList.OrderSortedByCustomerList = new ObservableCollection<Auftrag>();
        }


        //Instantiation of DM_DBConnection
        public static DM_DBConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DM_DBConnection();
                    }
                }
                return instance;
            }
        }


        //Methods

        //Address
        public bool addAddress(List<Adresse> addressList)
        {
            foreach (Adresse aAddress in addressList)
            {
                _carRentalEntities.AddToAdresse(aAddress);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateAddress(ObservableCollection<Adresse> addressList)
        {
            foreach (Adresse aAddress in addressList)
            {
                _carRentalEntities.UpdateObject(aAddress);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteAddress(List<Adresse> addressList)
        {
            foreach (Adresse aAddress in addressList)
            {
                _carRentalEntities.DeleteObject(aAddress);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //Login
        public bool addLogin(List<Anmeldung> loginList)
        {
            foreach (Anmeldung aLogin in loginList)
            {
                _carRentalEntities.AddToAnmeldung(aLogin);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateLogin(ObservableCollection<Anmeldung> loginList)
        {
            foreach (Anmeldung aLogin in loginList)
            {
                _carRentalEntities.UpdateObject(aLogin);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteLogin(List<Anmeldung> loginList)
        {
            foreach (Anmeldung aLogin in loginList)
            {
                _carRentalEntities.DeleteObject(aLogin);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //Order
        public bool addOrder(List<Auftrag> orderList)
        {
            foreach (Auftrag aOrder in orderList)
            {
                _carRentalEntities.AddToAuftrag(aOrder);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateOrder(ObservableCollection<Auftrag> orderList)
        {
            foreach (Auftrag aOrder in orderList)
            {
                _carRentalEntities.UpdateObject(aOrder);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteOrder(List<Auftrag> orderList)
        {
            foreach (Auftrag aOrder in orderList)
            {
                _carRentalEntities.DeleteObject(aOrder);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //User
        public bool addUser(List<Benutzer> userList)
        {
            foreach (Benutzer aUser in userList)
            {
                _carRentalEntities.AddToBenutzer(aUser);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateUser(ObservableCollection<Benutzer> userList)
        {
            foreach (Benutzer aUser in userList)
            {
                _carRentalEntities.UpdateObject(aUser);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteUser(List<Benutzer> userList)
        {
            foreach (Benutzer aUser in userList)
            {
                _carRentalEntities.DeleteObject(aUser);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //Vehicle
        public bool addVehicle(List<Fahrzeug> vehicleList)
        {
            foreach (Fahrzeug aVehicle in vehicleList)
            {
                _carRentalEntities.AddToFahrzeug(aVehicle);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateVehicle(ObservableCollection<Fahrzeug> vehicleList)
        {
            foreach (Fahrzeug aVehicle in vehicleList)
            {
                _carRentalEntities.UpdateObject(aVehicle);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteVehicle(List<Fahrzeug> vehicleList)
        {
            foreach (Fahrzeug aVehicle in vehicleList)
            {
                _carRentalEntities.DeleteObject(aVehicle);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //Contact
        public bool addContact(List<Kontakt> contactList)
        {
            foreach (Kontakt aContact in contactList)
            {
                _carRentalEntities.AddToKontakt(aContact);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateContact(ObservableCollection<Kontakt> contactList)
        {
            foreach (Kontakt aContact in contactList)
            {
                _carRentalEntities.UpdateObject(aContact);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteContact(List<Kontakt> contactList)
        {
            foreach (Kontakt aContact in contactList)
            {
                _carRentalEntities.DeleteObject(aContact);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }


        //Customer
        public bool addCustomer(List<Kunde> customerList)
        {
            foreach (Kunde aCustomer in customerList)
            {
                _carRentalEntities.AddToKunde(aCustomer);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool updateCustomer(ObservableCollection<Kunde> customerList)
        {
            foreach (Kunde aCustomer in customerList)
            {
                _carRentalEntities.UpdateObject(aCustomer);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }

        public bool deleteCustomer(List<Kunde> customerList)
        {
            foreach (Kunde aCustomer in customerList)
            {
                _carRentalEntities.DeleteObject(aCustomer);
            }
            _carRentalEntities.SaveChanges();
            return true;
        }
    }
}
