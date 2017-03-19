//using CarRental.CarRentalServiceReference;
using CarRental.CarRentalSchoolServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental
{ 
    public partial class GUI_MainMenu : Window
    {
        DM_DBConnection databaseConnection;
        CL_List list;

        public GUI_MainMenu()
        {
            InitializeComponent();
        }

        public GUI_MainMenu(string usertype, string username)
        {
            InitializeComponent();
            Initialize(usertype, username);
        }

        public void Initialize(string usertype, string username)
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            setGuiValues(usertype, username);            
        }

        private void setGuiValues(String usertype, String username)
        {
            labelActiveUsername.Content = username;
            labelActiveUsertype.Content = usertype;

            switch (usertype)
            {
                 case "Admin":
                    buttonUserManagement.IsEnabled = true;
                    buttonVehicleManagement.IsEnabled = true;
                    break;                                        
            }


               
        }

        private void buttonCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_CustomerManagement().Show();
        }

        private void buttonOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_OrderManagement().Show();
        }

        private void buttonUserManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_UserManagement().Show();
        }

        private void buttonVehicleManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_VehicleManagement().Show();
        }

        private void buttonSaveToDb_Click(object sender, RoutedEventArgs e)
        {       
            List<Adresse> addressList = databaseUpdateAddress();
            List<Kontakt> contactList = databaseUpdateContact();
            databaseUpdateCustomer(addressList, contactList);
            databaseUpdateVehicle();
            databaseUpdateOrder();
            List<Anmeldung> loginList = databaseUpdateLogin();
            databaseUpdateUser(loginList);

            MessageBox.Show("Datenbank erfolgreich aktualisiert.");
        } 

        private List<Adresse> databaseUpdateAddress()
        {
            List<Adresse> addressAddList = new List<Adresse>();

            if (!list.AddressList.Equals(list.AddressComparisonList))
            {

                foreach (Adresse address in list.AddressList)
                {
                    if (!list.AddressComparisonList.Contains(address))
                    {
                        addressAddList.Add(address);
                    }
                }

                if (addressAddList.Count != 0)
                {
                    databaseConnection.addAddress(addressAddList);
                    databaseConnection.updateAddress(list.AddressList);
                }
                else
                {
                    databaseConnection.updateAddress(list.AddressList);
                }
            }

            return addressAddList;
        }

        private List<Kontakt> databaseUpdateContact()
        {
            List<Kontakt> contactAddList = new List<Kontakt>();

            if (!list.ContactList.Equals(list.ContactComparisonList))
            {
                foreach (Kontakt contact in list.ContactList)
                {
                    if (!list.ContactComparisonList.Contains(contact))
                    {
                        contactAddList.Add(contact);
                    }
                }

                if (contactAddList.Count != 0)
                {
                    databaseConnection.addContact(contactAddList);
                    databaseConnection.updateContact(list.ContactList);
                }
                else
                {
                    databaseConnection.updateContact(list.ContactList);
                }
            }

            return contactAddList;
        }

        private void databaseUpdateCustomer(List<Adresse> addressAddList, List<Kontakt> contactAddList)
        {
            List<Kunde> customerAddList = new List<Kunde>();

            if (!list.CustomerList.Equals(list.CustomerComparisonList))
            {
                foreach (Kunde customer in list.CustomerList)
                {
                    if (!list.CustomerComparisonList.Contains(customer))
                    {
                        customerAddList.Add(customer);
                    }
                }

                int addressIndex = 0;
                foreach (Adresse address in addressAddList)
                {
                    customerAddList[addressIndex].AdresseID = address.AdresseID;
                    addressIndex++;
                }

                int contactIndex = 0;
                foreach (Kontakt contact in contactAddList)
                {
                    customerAddList[contactIndex].KontaktID = contact.KontaktID;
                    contactIndex++;
                }

                if (customerAddList.Count != 0)
                {
                    databaseConnection.addCustomer(customerAddList);
                    databaseConnection.updateCustomer(list.CustomerList);
                }
                else
                {
                    databaseConnection.updateCustomer(list.CustomerList);
                }
            }
        }

        private void databaseUpdateVehicle()
        {
            List<Fahrzeug> vehicleAddList = new List<Fahrzeug>();
            List<Fahrzeug> vehicleDeleteList = new List<Fahrzeug>();

            if (!list.VehicleList.Equals(list.VehicleComparisonList))
            {
                foreach (Fahrzeug vehicle in list.VehicleList)
                {
                    if (!list.VehicleComparisonList.Contains(vehicle))
                    {
                        vehicleAddList.Add(vehicle);
                    }
                }

                foreach (Fahrzeug vehicle in list.VehicleComparisonList)
                {
                    if (!list.VehicleList.Contains(vehicle))
                    {
                        vehicleDeleteList.Add(vehicle);
                    }
                }

                if (vehicleAddList.Count != 0)
                {
                    databaseConnection.addVehicle(vehicleAddList);

                    if (vehicleDeleteList.Count != 0)
                    {
                        databaseConnection.deleteVehicle(vehicleDeleteList);
                    }

                    databaseConnection.updateVehicle(list.VehicleList);
                }
                else
                {
                    if (vehicleDeleteList.Count != 0)
                    {
                        databaseConnection.deleteVehicle(vehicleDeleteList);
                    }

                    databaseConnection.updateVehicle(list.VehicleList);
                }
            }
        }

        private void databaseUpdateOrder()
        {
            List<Auftrag> orderDeleteList = new List<Auftrag>();
            List<Auftrag> orderAddList = new List<Auftrag>();

            if (!list.OrderList.Equals(list.OrderComparisonList))
            {
                foreach (Auftrag order in list.OrderList)
                {
                    if (!list.OrderComparisonList.Contains(order))
                    {
                        orderAddList.Add(order);
                    }
                }               

                foreach (Auftrag order in list.OrderComparisonList)
                {
                    if (!list.OrderList.Contains(order))
                    {
                        orderDeleteList.Add(order);
                    }
                }

                if (orderAddList.Count != 0)
                {
                    databaseConnection.addOrder(orderAddList);

                    if (orderDeleteList.Count != 0)
                    {
                        databaseConnection.deleteOrder(orderDeleteList);
                    }
                }
                else
                {
                    if (orderDeleteList.Count != 0)
                    {
                        databaseConnection.deleteOrder(orderDeleteList);
                    }
                }
            }
        }

        private List<Anmeldung> databaseUpdateLogin()
        {
            List<Anmeldung> loginAddList = new List<Anmeldung>();
            List<Anmeldung> loginDeleteList = new List<Anmeldung>();

            if (!list.LoginList.Equals(list.LoginComparisonList))
            {
                foreach (Anmeldung login in list.LoginList)
                {
                    if (!list.LoginComparisonList.Contains(login))
                    {
                        loginAddList.Add(login);
                    }
                }

                foreach (Anmeldung login in list.LoginComparisonList)
                {
                    if (!list.LoginList.Contains(login))
                    {
                        loginDeleteList.Add(login);
                    }
                }

                if (loginAddList.Count != 0)
                {
                    databaseConnection.addLogin(loginAddList);

                    if (loginDeleteList.Count != 0)
                    {
                        databaseConnection.deleteLogin(loginDeleteList);
                    }

                    databaseConnection.updateLogin(list.LoginList);
                }
                else
                {
                    if (loginDeleteList.Count != 0)
                    {
                        databaseConnection.deleteLogin(loginDeleteList);
                    }

                    databaseConnection.updateLogin(list.LoginList);
                }
            }

            return loginAddList;
        }

        private void databaseUpdateUser(List<Anmeldung> loginAddList)
        {
            List<Benutzer> userAddList = new List<Benutzer>();
            List<Benutzer> userDeleteList = new List<Benutzer>();

            if (!list.UserList.Equals(list.UserComparisonList))
            {
                foreach (Benutzer user in list.UserList)
                {
                    if (!list.UserComparisonList.Contains(user))
                    {
                        userAddList.Add(user);
                    }
                }

                int loginIndex = 0;
                foreach (Anmeldung login in loginAddList)
                {
                    userAddList[loginIndex].AnmeldeID = login.AnmeldungID;
                    loginIndex++;
                }

                foreach (Benutzer user in list.UserComparisonList)
                {
                    if (!list.UserList.Contains(user))
                    {
                        userDeleteList.Add(user);
                    }
                }

                if (userAddList.Count != 0)
                {
                    databaseConnection.addUser(userAddList);

                    if (userDeleteList.Count != 0)
                    {
                        databaseConnection.deleteUser(userDeleteList);
                    }

                    databaseConnection.updateUser(list.UserList);
                }
                else
                {
                    if (userDeleteList.Count != 0)
                    {
                        databaseConnection.deleteUser(userDeleteList);
                    }

                    databaseConnection.updateUser(list.UserList);
                }
            }
        }
    }    
}
