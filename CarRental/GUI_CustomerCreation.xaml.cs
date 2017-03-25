using CarRental.CarRentalServiceReference;
//using CarRental.CarRentalSchoolServiceReference;
//using CarRental.CarRentalEbertsonServiceReference;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class GUI_CustomerCreation : Window
    {
        #region Variables

        DM_DBConnection databaseConnection;
        CL_List list;
        Adresse aAddress;
        Adresse placeHolderAddress;       
        Kontakt aContact;
        Kontakt placeHolderContact;
        List<string> customerNumbers;
        string errorMessage;
        bool newAddress;
        bool newContact;
        #endregion

        #region Constructor

        public GUI_CustomerCreation()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Logic

        public void Initialize()
        {            
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            customerNumbers = null;
            errorMessage = null;
            aAddress = null;
            placeHolderAddress = null;
            aContact = null;
            placeHolderContact = null;       
            newAddress = false;
            newContact = false;            
            LoadComboBoxAddress();
            LoadComboBoxContact();
            CustomerNumberSequence();
            comboBoxGender.ItemsSource = new List<string>{ "Männlich", "Weiblich" };            
        }

        private void LoadComboBoxAddress()
        {
            placeHolderAddress = list.AddressList.FirstOrDefault(address => address.Ort == "Neue Adresse");

            if (placeHolderAddress == null)
            {
                placeHolderAddress = new Adresse { Ort = "Neue Adresse" };
                list.addToAddressList((placeHolderAddress));

                comboBoxAddress.Items.Clear();
                foreach (Adresse address in list.AddressList)
                {
                    comboBoxAddress.Items.Add(address);
                }
                comboBoxAddress.SelectedIndex = 0;
            }
            else
            {
                comboBoxAddress.Items.Clear();
                foreach (Adresse address in list.AddressList)
                {
                    comboBoxAddress.Items.Add(address);
                }
                comboBoxAddress.SelectedIndex = 0;
            }
        }

        private void LoadComboBoxContact()
        {
            placeHolderContact = list.ContactList.FirstOrDefault(contact => contact.E_Mail == "Neuer Kontakt");

            if (placeHolderContact == null)
            {
                placeHolderContact = new Kontakt { E_Mail = "Neuer Kontakt" };
                list.addToContactList((placeHolderContact));

                comboBoxContact.Items.Clear();
                foreach (Kontakt contact in list.ContactList)
                {
                    comboBoxContact.Items.Add(contact);
                }
                comboBoxContact.SelectedIndex = 0;
            }
            else
            {
                comboBoxContact.Items.Clear();
                foreach (Kontakt contact in list.ContactList)
                {
                    comboBoxContact.Items.Add(contact);
                }
                comboBoxContact.SelectedIndex = 0;
            }
        }

        private void CreateCustomer()
        {
            Kunde newCustomer = null;
            errorMessage = null;
            bool textHasSpace = false;

            if (newAddress)
            {
                if (!textBoxStreet.Text.Contains(" ") && !textBoxHouseNumber.Text.Contains(" ") &&
                    !textBoxZipCode.Text.Contains(" ") && !textBoxCity.Text.Contains(" "))
                {
                    if (!textBoxStreet.Text.Equals("") && !textBoxHouseNumber.Text.Equals("") &&
                        !textBoxZipCode.Text.Equals("") && !textBoxCity.Text.Equals(""))
                    {
                        aAddress = new Adresse();
                        aAddress.AdresseID = 0;
                        aAddress.Strasse = textBoxStreet.Text;
                        aAddress.Hausnummer = textBoxHouseNumber.Text;
                        aAddress.PLZ = textBoxZipCode.Text;
                        aAddress.Ort = textBoxCity.Text;
                    }
                    else
                    {
                        aAddress = null;
                        errorMessage += "Address";
                    }
                }
                else
                {
                    aAddress = null;
                    textHasSpace = true;
                    MessageBox.Show("In Ihren Adressdaten dürfen keine Leerzeichen enthalten sein.");
                }
            }

            if (newContact)
            {
                if (!textBoxMail.Text.Contains(" ") && !textBoxPhoneNumber.Text.Contains(" ") &&
                    !textBoxMobileNumber.Text.Contains(" ") && !textBoxFaxNumber.Text.Contains(" "))
                {
                    if (!textBoxMail.Text.Equals("") || !textBoxPhoneNumber.Text.Equals("") ||
                       !textBoxMobileNumber.Text.Equals("") || !textBoxFaxNumber.Text.Equals(""))
                    {
                        aContact = new Kontakt();
                        aContact.KontaktID = 0;
                        aContact.E_Mail = textBoxMail.Text;
                        aContact.Telefonnummer = textBoxPhoneNumber.Text;
                        aContact.Mobilnummer = textBoxMobileNumber.Text;
                        aContact.Faxnummer = textBoxFaxNumber.Text;
                    }
                    else
                    {
                        aContact = null;
                        errorMessage += "Contact";
                    }
                }
                else
                {
                    aContact = null;
                    textHasSpace = true;
                    MessageBox.Show("In Ihren Kontaktdaten dürfen keine Leerzeichen enthalten sein.");
                }
            }

            if (!textBoxCustomerNumber.Text.Contains(" ") && !textBoxFirstName.Text.Contains(" ") &&
                !textBoxLastName.Text.Contains(" ") && !textBoxAge.Text.Contains(" "))
            {
                if (!textBoxCustomerNumber.Text.Equals("") && !textBoxFirstName.Text.Equals("") && !textBoxLastName.Text.Equals("") &&
                    !comboBoxGender.Text.Equals("") && !textBoxAge.Text.Equals("") && !IsTextInputGreaterThenOneHundred(textBoxAge.Text))
                {
                    if (aAddress != null && aContact != null)
                    {
                        newCustomer = new Kunde();
                        newCustomer.KundeID = 0;
                        newCustomer.Kundennummer = textBoxCustomerNumber.Text;
                        newCustomer.Vornamen = textBoxFirstName.Text;
                        newCustomer.Nachnamen = textBoxLastName.Text;
                        newCustomer.Geschlecht = comboBoxGender.Text;
                        newCustomer.Alter = Convert.ToInt32(textBoxAge.Text);
                        newCustomer.Adresse = aAddress;
                        newCustomer.AdresseID = aAddress.AdresseID;
                        newCustomer.Kontakt = aContact;
                        newCustomer.KontaktID = aContact.KontaktID;
                    }
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

            switch (errorMessage)
            {
                case "AddressContactCustomer":
                    {
                        MessageBox.Show("Bitte füllen Sie die persönlichen Kunden-, Adress- und Kontaktdaten korrekt aus.");
                    }
                    break;
                case "AddressCustomer":
                    {
                        MessageBox.Show("Bitte füllen Sie die persönlichen Kunden- und Adressdaten korrekt aus.");
                    }
                    break;
                case "ContactCustomer":
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

            if (errorMessage == null && textHasSpace == false)
            {
                var result = MessageBox.Show("Möchten Sie einen neuen Kunden anlegen?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    list.AddressList.Remove(placeHolderAddress);
                    list.addToAddressList(aAddress);

                    list.ContactList.Remove(placeHolderContact);
                    list.addToContactList(aContact);

                    list.addToCustomerList(newCustomer);

                    LoadComboBoxAddress();
                    LoadComboBoxContact();
                    ClearComponents();
                    CustomerNumberSequence();
                }
            }
        }

        private void UpdateContactTextBoxes()
        {
            aContact = (Kontakt)comboBoxContact.SelectedItem;

            if (aContact != null)
            {
                if (aContact.E_Mail.Equals("Neuer Kontakt"))
                {
                    EnableContactComponents(true);
                    newContact = true;

                    textBoxMail.Text = null;
                    textBoxPhoneNumber.Text = null;
                    textBoxMobileNumber.Text = null;
                    textBoxFaxNumber.Text = null;
                }
                else
                {
                    EnableContactComponents(false);
                    newContact = false;

                    textBoxMail.Text = aContact.E_Mail;
                    textBoxPhoneNumber.Text = aContact.Telefonnummer;
                    textBoxMobileNumber.Text = aContact.Mobilnummer;
                    textBoxFaxNumber.Text = aContact.Faxnummer;
                }
            }
        }

        private void UpdateAddressTextBoxes()
        {
            aAddress = (Adresse)comboBoxAddress.SelectedItem;

            if (aAddress != null)
            {
                if (aAddress.Ort.Equals("Neue Adresse"))
                {
                    EnableAddressComponents(true);
                    newAddress = true;

                    textBoxStreet.Text = null;
                    textBoxHouseNumber.Text = null;
                    textBoxZipCode.Text = null;
                    textBoxCity.Text = null;
                }
                else
                {
                    EnableAddressComponents(false);
                    newAddress = false;

                    textBoxStreet.Text = aAddress.Strasse;
                    textBoxHouseNumber.Text = aAddress.Hausnummer;
                    textBoxZipCode.Text = aAddress.PLZ;
                    textBoxCity.Text = aAddress.Ort;
                }
            }
        }

        private void EnableAddressComponents(bool status)
        {
            textBoxStreet.IsEnabled = status;
            textBoxHouseNumber.IsEnabled = status;
            textBoxZipCode.IsEnabled = status;
            textBoxCity.IsEnabled = status;
        }

        private void EnableContactComponents(bool status)
        {
            textBoxMail.IsEnabled = status;
            textBoxPhoneNumber.IsEnabled = status;
            textBoxMobileNumber.IsEnabled = status;
            textBoxFaxNumber.IsEnabled = status;
        }

        private void ClearComponents()
        {
            textBoxCustomerNumber.Text = null;
            textBoxFirstName.Text = null;
            textBoxLastName.Text = null;
            comboBoxGender.Text = null;
            textBoxAge.Text = null;
        }     

        private bool IsTextInputGreaterThenOneHundred(string text)
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

        private bool IsTextInputTypeOfInteger(string text)
        {
            int number;
            return int.TryParse(text, out number);
        }
        
        private void CustomerNumberSequence()
        {            
            int customerNumber = 0;

            if(customerNumbers == null)
            {
                customerNumbers = new List<string>();
            }
            else
            {
                customerNumbers.Clear();
            }

            foreach(Kunde customer in list.CustomerList)
            {
                customerNumbers.Add(customer.Kundennummer.Trim('K','D'));
            }

            customerNumber = Convert.ToInt32(customerNumbers.Max());
            customerNumber++; 

            textBoxCustomerNumber.Text = "KD" + customerNumber.ToString("D5");
        }

        private void RemovePlaceHolderFromList()
        {
            list.AddressList.Remove(placeHolderAddress);
            list.ContactList.Remove(placeHolderContact);
        }
        #endregion

        #region Events

        private void ButtonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {
            CreateCustomer();
        }        

        private void ComboBoxAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAddressTextBoxes();
        }

        private void ComboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContactTextBoxes();
        }     

        private void ComboBoxAddress_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoadComboBoxAddress();
        }

        private void ComboBoxContact_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LoadComboBoxContact();
        }

        private void TextBoxAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RemovePlaceHolderFromList();
        }
        #endregion
    }
}