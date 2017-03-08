using CarRental.CarRentalServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    interface DM_IDBConnection
    {
        //Address
        bool addAddress(List<Adresse> addressList);
        bool updateAddress(ObservableCollection<Adresse> addressList);
        bool deleteAddress(List<Adresse> addressList);

        //Login
        bool addLogin(List<Anmeldung> loginList);
        bool updateLogin(ObservableCollection<Anmeldung> loginList);
        bool deleteLogin(List<Anmeldung> loginList);

        //Order
        bool addOrder(List<Auftrag> orderList);
        bool updateOrder(ObservableCollection<Auftrag> orderList);
        bool deleteOrder(List<Auftrag> orderList);

        //User
        bool addUser(List<Benutzer> userList);
        bool updateUser(ObservableCollection<Benutzer> userList);
        bool deleteUser(List<Benutzer> userList);

        //Vehicle
        bool addVehicle(List<Fahrzeug> vehicleList);
        bool updateVehicle(ObservableCollection<Fahrzeug> vehicleList);
        bool deleteVehicle(List<Fahrzeug> vehicleList);

        //Contact
        bool addContact(List<Kontakt> contactList);
        bool updateContact(ObservableCollection<Kontakt> contactList);
        bool deleteContact(List<Kontakt> contactList);

        //Customer
        bool addCustomer(List<Kunde> customerList);
        bool updateCustomer(ObservableCollection<Kunde> customerList);
        bool deleteCustomer(List<Kunde> customerList);
    }
}
