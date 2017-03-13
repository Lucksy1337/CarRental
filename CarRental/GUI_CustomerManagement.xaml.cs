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
    public partial class GUI_CustomerManagement : Window
    {
        DM_DBConnection databaseConnection;
        CL_List list;
        Kunde aCustomer;
        bool customerExists;
        string aCustomerNumber;

        public GUI_CustomerManagement()
        {
            InitializeComponent();
            Initialize();         
        }

        public void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            aCustomer = null;
            customerExists = false;
            aCustomerNumber = null;            
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            aCustomerNumber = textBoxCustomerNumberSearch.Text;

            foreach(Kunde customer in list.CustomerList)
            {
                if(customer.Kundennummer.Equals(aCustomerNumber))
                {
                    aCustomer = customer;
                    customerExists = true;
                }
            }

            if(customerExists)    
            {  
                foreach(Auftrag aOrder in list.OrderList)
                {
                    if (aOrder.Kunde.Equals(aCustomer))
                    {
                        list.addToOrderSortedByCustomerList(aOrder);
                    }
                }

                foreach(Fahrzeug aVehicle in list.VehicleList)
                {
                    if(aVehicle.Verfügbar)
                    {
                        list.addToVehicleSortedByOrderList(aVehicle);
                    }
                }

                comboBoxOrder.Items.Clear();
                foreach(Auftrag order in list.OrderSortedByCustomerList)
                {
                    comboBoxOrder.Items.Add(order);
                }                
                comboBoxOrder.SelectedIndex = 0;
                comboBoxVehicle.Items.Clear();
                foreach(Fahrzeug vehicle in list.VehicleSortedByOrderList)
                {
                    comboBoxVehicle.Items.Add(vehicle);
                }                           

                textBoxCustomerNumber.Text = aCustomer.Kundennummer;
                textBoxFirstName.Text = aCustomer.Vornamen;
                textBoxLastName.Text = aCustomer.Nachnamen;
                textBoxGender.Text = aCustomer.Geschlecht;
                textBoxAge.Text = Convert.ToString(aCustomer.Alter);

                textBoxStreet.Text = aCustomer.Adresse.Strasse;
                textBoxHouseNumber.Text = aCustomer.Adresse.Hausnummer;
                textBoxZipCode.Text = aCustomer.Adresse.PLZ;
                textBoxCity.Text = aCustomer.Adresse.Ort;

                textBoxMail.Text = aCustomer.Kontakt.E_Mail;
                textBoxPhoneNumber.Text = aCustomer.Kontakt.Telefonnummer;
                textBoxMobileNumber.Text = aCustomer.Kontakt.Mobilnummer;
                textBoxFaxNumber.Text = aCustomer.Kontakt.Faxnummer;

                EnableComponents();
            }
            else
            {
                MessageBox.Show("Kunde mit der Kundennummer " + aCustomerNumber + " wurde nicht gefunden.");
            }
        }

        private void comboBoxOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            Auftrag aOrder = (Auftrag)comboBoxOrder.SelectedItem;            
            list.addToVehicleSortedByOrderList(aOrder.Fahrzeug);
            comboBoxVehicle.SelectedItem = aOrder.Fahrzeug;
            textBoxTotalPrice.Text = Convert.ToString(aOrder.Gesamtpreis);
            datePickerOrderDate.SelectedDate = aOrder.Auftragsdatum;
            datePickerReturnDate.SelectedDate = aOrder.Rückgabedatum; 
        }       

        private void comboBoxVehicle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeSpan timespan = Convert.ToDateTime(datePickerReturnDate.SelectedDate) - Convert.ToDateTime(datePickerOrderDate.SelectedDate);  
            Fahrzeug aVehicle = (Fahrzeug)comboBoxVehicle.SelectedItem;
            double totalPrice = aVehicle.MietpreisProTag * timespan.Days;
            textBoxTotalPrice.Text = totalPrice.ToString("C");
        }   

        private void buttonNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            GUI_CustomerCreation formCustomerCreation = new GUI_CustomerCreation();
            formCustomerCreation.Show();
        }

        private void buttonNewAddress_Click(object sender, RoutedEventArgs e)
        {
            GUI_AddressManagement formAddressManagement = new GUI_AddressManagement();
            formAddressManagement.Show();
        }

        private void buttonNewContact_Click(object sender, RoutedEventArgs e)
        {
            GUI_ContactManagement formContactManagement = new GUI_ContactManagement();
            formContactManagement.Show();
        }

        private void textBoxAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && isTextInputTypeOfInteger(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private bool isTextInputTypeOfInteger(string text)
        {
            int number;

            return int.TryParse(text, out number);
        }

        private void EnableComponents()
        {
            buttonSave.IsEnabled = true;
            textBoxFirstName.IsEnabled = true;
            textBoxLastName.IsEnabled = true;
            textBoxGender.IsEnabled = true;
            textBoxAge.IsEnabled = true;

            textBoxStreet.IsEnabled = true;
            textBoxHouseNumber.IsEnabled = true;
            textBoxZipCode.IsEnabled = true;
            textBoxCity.IsEnabled = true;

            textBoxMail.IsEnabled = true;
            textBoxPhoneNumber.IsEnabled = true;
            textBoxMobileNumber.IsEnabled = true;
            textBoxFaxNumber.IsEnabled = true;            
            
            if(list.OrderSortedByCustomerList.Count!=0)
            {
                buttonCancelOrder.IsEnabled = true;
                comboBoxOrder.IsEnabled = true;
                comboBoxVehicle.IsEnabled = true;
                datePickerOrderDate.IsEnabled = true;
                datePickerReturnDate.IsEnabled = true;
            }            
        }  
    }
}