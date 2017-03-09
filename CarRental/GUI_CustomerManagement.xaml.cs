using CarRental.CarRentalServiceReference;
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
        ToolTip too;

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
                EnableComponents();

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

                comboBoxOrder.ItemsSource = list.OrderSortedByCustomerList;
                comboBoxOrder.SelectedIndex = 0;
                comboBoxVehicle.ItemsSource = list.VehicleSortedByOrderList;                

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
            }
            else
            {
                MessageBox.Show("Kunde mit der Kundennummer " + aCustomerNumber + " wurde nicht gefunden.");
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            Auftrag aOrder = (Auftrag)comboBoxOrder.SelectedItem;            
            list.addToVehicleSortedByOrderList(aOrder.Fahrzeug);
            comboBoxVehicle.SelectedItem = aOrder.Fahrzeug;
            textBoxTotalPrice.Text = Convert.ToString(aOrder.Gesamtpreis);
            datePickerOrderDate.SelectedDate = aOrder.Auftragsdatum;
            datePickerReturnDate.SelectedDate = aOrder.Rückgabedatum; 
        }

        public void EnableComponents()
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

            buttonCancelOrder.IsEnabled = true;
            comboBoxOrder.IsEnabled = true;
            comboBoxVehicle.IsEnabled = true;            
            datePickerOrderDate.IsEnabled = true;
            datePickerReturnDate.IsEnabled = true;
        }
    }
}

