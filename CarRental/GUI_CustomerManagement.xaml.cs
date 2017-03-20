using CarRental.CarRentalServiceReference;
//using CarRental.CarRentalSchoolServiceReference;
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
        Auftrag aOrder;
        bool customerExists;
        string aCustomerNumber;
        string errorMessage;

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
            aOrder = null;
            customerExists = false;
            aCustomerNumber = null;
            errorMessage = null;            
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxCustomerNumberSearch.Text.Contains(" "))
            {
                aCustomerNumber = textBoxCustomerNumberSearch.Text;

                foreach (Kunde customer in list.CustomerList)
                {
                    if (customer.Kundennummer.Equals(aCustomerNumber))
                    {
                        aCustomer = customer;
                        customerExists = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Ihre Kundennummer darf keine Leerzeichen enthalten.");
            }

            if (customerExists)
            {
                list.OrderSortedByCustomerList.Clear();
                foreach (Auftrag aOrder in list.OrderList)
                {
                    if (aOrder.Kunde.Equals(aCustomer))
                    {
                        list.addToOrderSortedByCustomerList(aOrder);
                    }
                }

                comboBoxOrder.Items.Clear();
                foreach (Auftrag order in list.OrderSortedByCustomerList)
                {
                    comboBoxOrder.Items.Add(order);
                }
                comboBoxOrder.SelectedIndex = 0;                

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

                enableComponents();
            }
            else if(!aCustomerNumber.Equals(""))
            {
                MessageBox.Show("Kunde mit der Kundennummer " + aCustomerNumber + " wurde nicht gefunden.");
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            errorMessage = null;
            bool textHasSpace = false;

            if (!textBoxCustomerNumber.Text.Contains(" ") && !textBoxFirstName.Text.Contains(" ") && !(textBoxLastName.Text).Contains(" ") &&
                !textBoxGender.Text.Contains(" ") && !textBoxAge.Text.Contains(" "))
            {
                if (!textBoxCustomerNumber.Text.Equals("") && !textBoxFirstName.Text.Equals("") && !(textBoxLastName.Text).Equals("") &&
                   !textBoxGender.Text.Equals("") && !textBoxAge.Text.Equals("") && !isTextInputGreaterThenOneHundred(textBoxAge.Text))
                {
                    aCustomer.Vornamen = textBoxFirstName.Text;
                    aCustomer.Nachnamen = textBoxLastName.Text;
                    aCustomer.Geschlecht = textBoxGender.Text;
                    aCustomer.Alter = Convert.ToInt32(textBoxAge.Text);
                }
                else
                {
                    errorMessage += "Customer";
                }
            }
            else
            {
                textHasSpace = true;
                MessageBox.Show("In Ihren persönlichen Kundendaten dürfen keine Leerzeichen enthalten sein.");
            }


            if (!textBoxStreet.Text.Contains(" ") && !textBoxHouseNumber.Text.Contains(" ") &&
                !textBoxZipCode.Text.Contains(" ") && !textBoxCity.Text.Contains(" "))
            {
                if (!textBoxStreet.Text.Equals("") && !textBoxHouseNumber.Text.Equals("") &&
                   !textBoxZipCode.Text.Equals("") && !textBoxCity.Text.Equals(""))
                {
                    aCustomer.Adresse.Strasse = textBoxStreet.Text;
                    aCustomer.Adresse.Hausnummer = textBoxHouseNumber.Text;
                    aCustomer.Adresse.PLZ = textBoxZipCode.Text;
                    aCustomer.Adresse.Ort = textBoxCity.Text;
                }
                else
                {
                    errorMessage += "Address";
                }
            }
            else
            {
                textHasSpace = true;
                MessageBox.Show("In Ihren Adressdaten dürfen keine Leerzeichen enthalten sein.");
            }

            if (!textBoxMail.Text.Contains(" ") && !textBoxPhoneNumber.Text.Contains(" ") &&
                !textBoxMobileNumber.Text.Contains(" ") && !textBoxFaxNumber.Text.Contains(" "))
            {
                if (!textBoxMail.Text.Equals("") || !textBoxPhoneNumber.Text.Equals("") ||
                   !textBoxMobileNumber.Text.Equals("") || !textBoxFaxNumber.Text.Equals(""))
                {
                    aCustomer.Kontakt.E_Mail = textBoxMail.Text;
                    aCustomer.Kontakt.Telefonnummer = textBoxPhoneNumber.Text;
                    aCustomer.Kontakt.Mobilnummer = textBoxMobileNumber.Text;
                    aCustomer.Kontakt.Faxnummer = textBoxFaxNumber.Text;
                }
                else
                {
                    errorMessage += "Contact";
                }
            }
            else
            {
                textHasSpace = true;
                MessageBox.Show("In Ihren Kontaktdaten dürfen keine Leerzeichen enthalten sein.");
            }

            switch (errorMessage)
            {
                case "CustomerAddressContact":                
                {
                    MessageBox.Show("Bitte füllen Sie die persönlichen Kunden-, Adress- und Kontaktdaten korrekt aus.");
                }
                break;
                case "CustomerAddress":
                {
                    MessageBox.Show("Bitte füllen Sie die persönlichen Kunden- und Adressdaten korrekt aus.");
                }
                break;
                case "CustomerContact":
                {
                    MessageBox.Show("Bitte füllen Sie die persönlichen Kunden- und Kontaktdaten korrekt aus.");
                }
                break;
                case "Customer":
                {
                    MessageBox.Show("Bitte füllen Sie die persönlichen Kundendaten korrekt aus.");
                }
                break;
                case "AddressContact":
                {
                    MessageBox.Show("Bitte füllen Sie die Adress- und Kontaktdaten korrekt aus.");
                }
                break;
                case "Address":
                {
                    MessageBox.Show("Bitte füllen Sie die Adressdaten korrekt aus.");
                }
                break;
                case "Contact":
                {
                    MessageBox.Show("Bitte füllen Sie die Kontaktdaten korrekt aus.");
                }
                break;               
            }

            if(errorMessage == null && textHasSpace == false)
            {
                var result = MessageBox.Show("Möchten Sie die Kundendaten speichern?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Kunde oldCustomer = list.CustomerList.Where(customer => customer.Kundennummer.Equals(aCustomer.Kundennummer)).First();
                    list.CustomerList.Remove(oldCustomer);
                    list.addToCustomerList(aCustomer);

                    Adresse oldAddress = list.AddressList.Where(address => address.Equals(aCustomer.Adresse)).First();
                    list.AddressList.Remove(oldAddress);
                    list.addToAddressList(aCustomer.Adresse);

                    Kontakt oldContact = list.ContactList.Where(contact => contact.Equals(aCustomer.Kontakt)).First();
                    list.ContactList.Remove(oldContact);
                    list.addToContactList(aCustomer.Kontakt);

                    MessageBox.Show("Kundendaten wurden erfolgreich gespeichert.");
                }           
            }
        }

        private void comboBoxOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(list.OrderSortedByCustomerList.Count != 0)
            {
                if(aCustomer.Auftrag != null)
                {
                    aOrder = (Auftrag)comboBoxOrder.SelectedItem;
                    if(aOrder!=null)
                    {
                        textBoxVehicleDescription.Text = Convert.ToString(aOrder.Fahrzeug);
                        textBoxTotalPrice.Text = aOrder.Gesamtpreis.ToString("C");
                        datePickerOrderDate.SelectedDate = aOrder.Auftragsdatum;
                        datePickerReturnDate.SelectedDate = aOrder.Rückgabedatum;
                    }                   
                }
            }
        } 

        private void buttonNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            GUI_CustomerCreation formCustomerCreation = new GUI_CustomerCreation();
            formCustomerCreation.Show();
        }

        private void buttonCancelOrder_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Möchten Sie den ausgewählten Auftrag stornieren?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                aOrder = (Auftrag)comboBoxOrder.SelectedItem;
                list.OrderSortedByCustomerList.Remove(aOrder);
                list.OrderList.Remove(aOrder);
                                
                MessageBox.Show("Der ausgewählte Auftrag wurde storniert.");

                loadComboBoxOrder();
                enableComponents();
            }
        }

        private void loadComboBoxOrder()
        {
            comboBoxOrder.Items.Clear();
            foreach(Auftrag order in list.OrderSortedByCustomerList)
            {
                comboBoxOrder.Items.Add(order);
            }
            comboBoxOrder.SelectedIndex = 0;
        }

        private void enableComponents()
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

            if (list.OrderSortedByCustomerList.Count != 0)
            {
                buttonCancelOrder.IsEnabled = true;
                comboBoxOrder.IsEnabled = true;
            }
            else
            {
                buttonCancelOrder.IsEnabled = false;
                comboBoxOrder.IsEnabled = false;
                clearOrderComponents();
            }
        }

        private void clearOrderComponents()
        {
            comboBoxOrder.Items.Clear();
            textBoxVehicleDescription.Text = null;
            textBoxTotalPrice.Text = null;
            datePickerOrderDate.SelectedDate = null;
            datePickerReturnDate.SelectedDate = null;
        }

        private void textBoxAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!isTextInputTypeOfInteger(e.Text))
            {               
                e.Handled = true;
            }            
        }

        private bool isTextInputTypeOfInteger(string text)
        {
            int number;

            return int.TryParse(text, out number);
        }   

        private bool isTextInputGreaterThenOneHundred(string text)
        {            
            int number; Int32.TryParse(text, out number);
            bool status;

            if (number > 100)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }       
    }
}