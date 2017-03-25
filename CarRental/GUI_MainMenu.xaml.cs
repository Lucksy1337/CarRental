using CarRental.CarRentalServiceReference;
//using CarRental.CarRentalSchoolServiceReference;
//using CarRental.CarRentalEbertsonServiceReference;

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region Variables

        private GUI_CustomerManagement formCustomerManagement;
        private GUI_OrderManagement formOrderManagement;
        private GUI_UserManagement formUserManagement;
        private GUI_VehicleManagement formVehicleManagement;
        private GUI_AccountManagement formAccountManagement;     
        private string closingMessage;  
        private DM_DBConnection databaseConnection;
        private CL_List list;
        private bool customerManagementFormOpen;
        private bool orderManagementFormOpen;
        private bool userManagementFormOpen;
        private bool vehicleManagementFormOpen;
        private bool accountManagementFormOpen;
        #endregion

        #region Constructor

        public GUI_MainMenu()
        {
            InitializeComponent();
        }

        public GUI_MainMenu(string usertype, string username)
        {
            InitializeComponent();
            Initialize(usertype, username);
        }
        #endregion

        #region Logic

        public void Initialize(string usertype, string username)
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            closingMessage = "?";
            SetGuiValues(usertype, username);
            customerManagementFormOpen = false;
            orderManagementFormOpen = false;
            vehicleManagementFormOpen = false;
            accountManagementFormOpen = false;                           
        }

        private void SetGuiValues(String usertype, String username)
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

        private void OpenCustomerManagementForm()
        {
            BuildClosingMessage("Customer");
            formCustomerManagement = new GUI_CustomerManagement();
            formCustomerManagement.Show();
        }

        private void OpenOrderManagementForm()
        {
            BuildClosingMessage("Order");
            formOrderManagement = new GUI_OrderManagement();
            formOrderManagement.Show();
        }     

        private void OpenUserManagementForm()
        {
            BuildClosingMessage("User");
            formUserManagement = new GUI_UserManagement(labelActiveUsername.Content.ToString());
            formUserManagement.Show();
        }
        
        private void OpenVehicleManagementForm()
        {
            BuildClosingMessage("Vehicle");
            formVehicleManagement = new GUI_VehicleManagement();
            formVehicleManagement.Show();
        }

        private void OpenAccountManagementForm()
        {
            BuildClosingMessage("Account");
            formAccountManagement = new GUI_AccountManagement(labelActiveUsername.Content.ToString());
            formAccountManagement.Show();
        }     

        private void UpdateDatabase()
        {           
            DatabaseUpdateAddress();
            DatabaseUpdateContact();
            DatabaseUpdateCustomer();
            DatabaseUpdateVehicle();
            DatabaseUpdateOrder();
            DatabaseUpdateLogin();
            DatabaseUpdateUser();

            MessageBox.Show("Datenbank erfolgreich aktualisiert.");
        }

        private void DatabaseUpdateAddress()
        {
            if (list.AddressAddList == null)
            {
                list.AddressAddList = new List<Adresse>();
            }
            else
            {
                list.AddressAddList.Clear();
            }

            if (!list.AddressList.Equals(list.AddressComparisonList))
            {

                foreach (Adresse address in list.AddressList)
                {
                    if (!list.AddressComparisonList.Contains(address))
                    {
                        list.AddressAddList.Add(address);
                    }
                }

                if (list.AddressAddList.Count != 0)
                {
                    databaseConnection.addAddress(list.AddressAddList);                    
                }
                                
                databaseConnection.updateAddress(list.AddressList);
                list.AddressComparisonList = list.AddressList;
            }
        }

        private void DatabaseUpdateContact()
        {
            if (list.ContactAddList == null)
            {
                list.ContactAddList = new List<Kontakt>();
            }
            else
            {
                list.ContactAddList.Clear();
            }

            if (!list.ContactList.Equals(list.ContactComparisonList))
            {
                foreach (Kontakt contact in list.ContactList)
                {
                    if (!list.ContactComparisonList.Contains(contact))
                    {
                        list.ContactAddList.Add(contact);
                    }
                }

                if (list.ContactAddList.Count != 0)
                {
                    databaseConnection.addContact(list.ContactAddList);                    
                }
                               
                databaseConnection.updateContact(list.ContactList);
                list.ContactComparisonList = list.ContactList;
            }
        }

        private void DatabaseUpdateCustomer()
        {  
            if (list.CustomerAddList == null)
            {
                list.CustomerAddList = new List<Kunde>();
            }
            else
            {
                list.CustomerAddList.Clear();
            }

            if (!list.CustomerList.Equals(list.CustomerComparisonList))
            {
                foreach (Kunde customer in list.CustomerList)
                {
                    if (!list.CustomerComparisonList.Contains(customer))
                    {
                        list.CustomerAddList.Add(customer);
                    }
                }

                int addressIndex = 0;
                foreach (Adresse address in list.AddressAddList)
                {
                    list.CustomerAddList[addressIndex].AdresseID = address.AdresseID;
                    addressIndex++;
                }

                int contactIndex = 0;
                foreach (Kontakt contact in list.ContactAddList)
                {
                    list.CustomerAddList[contactIndex].KontaktID = contact.KontaktID;
                    contactIndex++;
                }

                if (list.CustomerAddList.Count != 0)
                {
                    databaseConnection.addCustomer(list.CustomerAddList);
                }
                                
                databaseConnection.updateCustomer(list.CustomerList);
                list.CustomerComparisonList = list.CustomerList;
            }
        }

        private void DatabaseUpdateVehicle()
        {
            if (list.VehicleAddList == null)
            {
                list.VehicleAddList = new List<Fahrzeug>();
            }
            else
            {
                list.VehicleAddList.Clear();
            }

            if (list.VehicleDeleteList == null)
            {
                list.VehicleDeleteList = new List<Fahrzeug>();
            }
            else
            {
                list.VehicleDeleteList.Clear();
            }

            if (!list.VehicleList.Equals(list.VehicleComparisonList))
            {
                foreach (Fahrzeug vehicle in list.VehicleList)
                {
                    if (!list.VehicleComparisonList.Contains(vehicle))
                    {
                        list.VehicleAddList.Add(vehicle);
                    }
                }

                foreach (Fahrzeug vehicle in list.VehicleComparisonList)
                {
                    if (!list.VehicleList.Contains(vehicle))
                    {
                        list.VehicleDeleteList.Add(vehicle);
                    }
                }

                if (list.VehicleAddList.Count != 0)
                {
                    databaseConnection.addVehicle(list.VehicleAddList);
                }                
                                   
                list.VehicleComparisonList = list.VehicleList;
                databaseConnection.updateVehicle(list.VehicleList);
            }
        }

        private void DatabaseUpdateOrder()
        {
            if (list.OrderAddList == null)
            {
                list.OrderAddList = new List<Auftrag>();
            }
            else
            {
                list.OrderAddList.Clear();
            }

            if (list.OrderDeleteList == null)
            {
                list.OrderDeleteList = new List<Auftrag>();
            }
            else
            {
                list.OrderDeleteList.Clear();
            }

            if (!list.OrderList.Equals(list.OrderComparisonList))
            {
                foreach (Auftrag order in list.OrderList)
                {
                    if (!list.OrderComparisonList.Contains(order))
                    {
                        list.OrderAddList.Add(order);
                    }
                }               

                foreach (Auftrag order in list.OrderComparisonList)
                {
                    if (!list.OrderList.Contains(order))
                    {
                        list.OrderDeleteList.Add(order);
                    }
                }

                foreach (Auftrag order in list.OrderAddList)
                {
                    foreach (Kunde customer in list.CustomerAddList)
                    {
                        if (customer.Equals(order.Kunde))
                        {
                            order.Kunde = customer;
                            order.KundeID = customer.KundeID;
                        }
                    }
                }

                foreach (Auftrag order in list.OrderAddList)
                {
                    foreach (Fahrzeug vehicle in list.VehicleAddList)
                    {
                        if (vehicle.Equals(order.Fahrzeug))
                        {
                            order.Fahrzeug = vehicle;
                            order.FahrzeugID = vehicle.FahrzeugID;
                        }
                    }
                }

                if (list.OrderAddList.Count != 0)
                {
                    databaseConnection.addOrder(list.OrderAddList);
                }

                if (list.OrderDeleteList.Count != 0)
                {
                    databaseConnection.deleteOrder(list.OrderDeleteList);
                }

                if (list.VehicleDeleteList.Count != 0)
                {
                    databaseConnection.deleteVehicle(list.VehicleDeleteList);
                }

                databaseConnection.updateOrder(list.OrderList);
                list.OrderComparisonList = list.OrderList;
            }
        }

        private void DatabaseUpdateLogin()
        {
            if (list.LoginAddList == null)
            {
                list.LoginAddList = new List<Anmeldung>();
            }
            else
            {
                list.LoginAddList.Clear();
            }

            if (list.LoginDeleteList == null)
            {
                list.LoginDeleteList = new List<Anmeldung>();
            }
            else
            {
                list.LoginDeleteList.Clear();
            }

            if (!list.LoginList.Equals(list.LoginComparisonList))
            {
                foreach (Anmeldung login in list.LoginList)
                {
                    if (!list.LoginComparisonList.Contains(login))
                    {
                        list.LoginAddList.Add(login);
                    }
                }

                foreach (Anmeldung login in list.LoginComparisonList)
                {
                    if (!list.LoginList.Contains(login))
                    {
                        list.LoginDeleteList.Add(login);
                    }
                }

                if (list.LoginAddList.Count != 0)
                {
                    databaseConnection.addLogin(list.LoginAddList);
                }               
                                
                databaseConnection.updateLogin(list.LoginList);
                list.LoginComparisonList = list.LoginList;
            }
        }

        private void DatabaseUpdateUser()
        {
            if (list.UserAddList == null)
            {
                list.UserAddList = new List<Benutzer>();
            }
            else
            {
                list.UserAddList.Clear();
            }

            if (list.UserDeleteList == null)
            {
                list.UserDeleteList = new List<Benutzer>();
            }
            else
            {
                list.UserDeleteList.Clear();
            }

            if (!list.UserList.Equals(list.UserComparisonList))
            {
                foreach (Benutzer user in list.UserList)
                {
                    if (!list.UserComparisonList.Contains(user))
                    {
                        list.UserAddList.Add(user);
                    }
                }

                int loginIndex = 0;
                foreach (Anmeldung login in list.LoginAddList)
                {
                    list.UserAddList[loginIndex].AnmeldeID = login.AnmeldungID;
                    loginIndex++;
                }

                foreach (Benutzer user in list.UserComparisonList)
                {
                    if (!list.UserList.Contains(user))
                    {
                        list.UserDeleteList.Add(user);
                    }
                }

                if (list.UserAddList.Count != 0)
                {
                    databaseConnection.addUser(list.UserAddList);
                }                

                if (list.UserDeleteList.Count != 0)
                {
                    databaseConnection.deleteUser(list.UserDeleteList);
                }

                if (list.LoginDeleteList.Count != 0)
                {
                    databaseConnection.deleteLogin(list.LoginDeleteList);
                }

                databaseConnection.updateUser(list.UserList);
                list.UserComparisonList = list.UserList;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!closingMessage.Contains("?"))
            {
                if (closingMessage.Split(':').Contains("Customer"))
                {
                    if (formCustomerManagement.IsLoaded)
                    {
                        e.Cancel = true;
                        customerManagementFormOpen = true;                   
                    }
                    else
                    {
                        formCustomerManagement.Close();
                        customerManagementFormOpen = false;                  
                    }
                }

                if (closingMessage.Split(':').Contains("Order"))
                {
                    if (formOrderManagement.IsLoaded)
                    {                        
                        e.Cancel = true;
                        orderManagementFormOpen = true;
                    }
                    else
                    {
                        formOrderManagement.Close();
                        orderManagementFormOpen = false;                   
                    }
                }

                if (closingMessage.Split(':').Contains("User"))
                {
                    if (formUserManagement.IsLoaded)
                    {                        
                        e.Cancel = true;
                        userManagementFormOpen = true;
                    }
                    else
                    {
                        userManagementFormOpen = false;
                        formUserManagement.Close();                  
                    }
                }

                if (closingMessage.Split(':').Contains("Vehicle"))
                {
                    if (formVehicleManagement.IsLoaded)
                    {                        
                        e.Cancel = true;
                        vehicleManagementFormOpen = true;
                    }
                    else
                    {
                        vehicleManagementFormOpen = false;
                        formVehicleManagement.Close();                       
                    }
                }

                if (closingMessage.Split(':').Contains("Account"))
                {
                    if (formAccountManagement.IsLoaded)
                    {                       
                        e.Cancel = true;
                        accountManagementFormOpen = true;
                    }
                    else
                    {
                        accountManagementFormOpen = false;
                        formAccountManagement.Close();                        
                    }
                }

                if (!customerManagementFormOpen && !orderManagementFormOpen && !userManagementFormOpen &&
                    !vehicleManagementFormOpen && !accountManagementFormOpen)
                {
                    var result = MessageBox.Show("    Möchten Sie das Auswahlfenster schließen?" + Environment.NewLine + Environment.NewLine + "                              Warnung! " + Environment.NewLine + Environment.NewLine + "Nicht gespeicherte Änderungen gehen verloren.", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    MessageBox.Show("Sie müssen zuerst alle anderen Fenster schließen.");
                }
            }
            else
            {
                var result = MessageBox.Show("    Möchten Sie das Auswahlfenster schließen?" + Environment.NewLine + Environment.NewLine + "                              Warnung! " + Environment.NewLine + Environment.NewLine + "Nicht gespeicherte Änderungen gehen verloren.", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }           
        }

        private void BuildClosingMessage(string text)
        {
            if (Convert.ToInt32(closingMessage.Split(':').Contains("Customer")) < 1 || Convert.ToInt32(closingMessage.Split(':').Contains("Order")) < 1 ||
                Convert.ToInt32(closingMessage.Split(':').Contains("User")) < 1 || Convert.ToInt32(closingMessage.Split(':').Contains("Vehicle")) < 1 ||
                Convert.ToInt32(closingMessage.Split(':').Contains("Account")) < 1)
            {
                if (closingMessage.Contains("?"))
                {
                    closingMessage = null;
                    closingMessage += text;
                }
                else
                {
                    closingMessage += ":" + text;
                }
            }   
        }
        #endregion

        #region Events

        private void ButtonSaveToDatabase_Click(object sender, RoutedEventArgs e)
        {
            UpdateDatabase();
        }

        private void ButtonCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenCustomerManagementForm();
        }

        private void ButtonOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenOrderManagementForm();
        }

        private void ButtonUserManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenUserManagementForm();
        }

        private void ButtonVehicleManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenVehicleManagementForm();
        }

        private void ButtonAccountManagement_Click(object sender, RoutedEventArgs e)
        {
            OpenAccountManagementForm();
        }       
        #endregion
    }
}