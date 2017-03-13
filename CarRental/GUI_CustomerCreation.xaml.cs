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
    public partial class GUI_CustomerCreation : Window
    {
        DM_DBConnection databaseConnection;
        CL_List list;
        Adresse aAddress;
        Adresse placeHolderAddress;       
        Kontakt aContact;
        Kontakt placeHolderContact;
        bool newAddress;
        bool newContact;


        public GUI_CustomerCreation()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            aAddress = null;
            placeHolderAddress = null;         
            aContact = null;
            placeHolderContact = null;       
            newAddress = false;
            newContact = false;
            loadComboBoxAddress();
            loadComboBoxContact();
        }

        private void comboBoxAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aAddress = (Adresse)comboBoxAddress.SelectedItem;

            if(aAddress!=null)
            {
                if(aAddress.Ort.Equals("Neue Adresse"))
                {
                    enableAddressComponents(true);
                    newAddress = true;

                    textBoxStreet.Text = null;
                    textBoxHouseNumber.Text = null;
                    textBoxZipCode.Text = null;
                    textBoxCity.Text = null; 
                }
                else
                {
                    enableAddressComponents(false);
                    newAddress = false;

                    textBoxStreet.Text = aAddress.Strasse;
                    textBoxHouseNumber.Text = aAddress.Hausnummer;
                    textBoxZipCode.Text = aAddress.PLZ;
                    textBoxCity.Text = aAddress.Ort;
                }
            }
        }

        private void comboBoxContact_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            aContact = (Kontakt)comboBoxContact.SelectedItem;

            if(aContact!=null)
            {
                if (aContact.E_Mail.Equals("Neuer Kontakt"))
                {
                    enableContactComponents(true);
                    newContact = true;

                    textBoxMail.Text = null;
                    textBoxPhoneNumber.Text = null;
                    textBoxMobileNumber.Text = null;
                    textBoxFaxNumber.Text = null;
                }
                else
                {
                    enableContactComponents(false);
                    newContact = false;

                    textBoxMail.Text = aContact.E_Mail;
                    textBoxPhoneNumber.Text = aContact.Telefonnummer;
                    textBoxMobileNumber.Text = aContact.Mobilnummer;
                    textBoxFaxNumber.Text = aContact.Faxnummer;
                }
            }
        }    

        private void buttonCreateCustomer_Click(object sender, RoutedEventArgs e)
        {  
            if (!textBoxCustomerNumber.Text.Equals("") || !textBoxFirstName.Text.Equals("") ||
                !textBoxLastName.Text.Equals("") || !textBoxGender.Text.Equals("") || !textBoxAge.Text.Equals(""))
            {
                if (newAddress)
                {
                    aAddress = new Adresse();
                    aAddress.AdresseID = 0;
                    aAddress.Strasse = textBoxStreet.Text;
                    aAddress.Hausnummer = textBoxHouseNumber.Text;
                    aAddress.PLZ = textBoxZipCode.Text;
                    aAddress.Ort = textBoxCity.Text;
                                        
                    list.AddressList.Remove(placeHolderAddress);                   
                    list.addToAddressList(aAddress);
                }

                if (newContact)
                {
                    aContact = new Kontakt();
                    aContact.KontaktID = 0;                    
                    aContact.E_Mail = textBoxMail.Text;
                    aContact.Telefonnummer = textBoxPhoneNumber.Text;
                    aContact.Mobilnummer = textBoxMobileNumber.Text;
                    aContact.Faxnummer = textBoxFaxNumber.Text;

                    list.ContactList.Remove(placeHolderContact);
                    list.addToContactList(aContact);
                }

                var result = MessageBox.Show("Möchten Sie einen neuen Kunden anlegen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {                    
                    Kunde newCustomer = new Kunde();
                    newCustomer.KundeID = 0;
                    newCustomer.Kundennummer = textBoxCustomerNumber.Text;
                    newCustomer.Vornamen = textBoxFirstName.Text;
                    newCustomer.Nachnamen = textBoxLastName.Text;
                    newCustomer.Geschlecht = textBoxGender.Text;
                    newCustomer.Alter = Convert.ToInt32(textBoxAge.Text);
                    if (aAddress != null)
                    {
                        newCustomer.Adresse = aAddress;
                        newCustomer.AdresseID = aAddress.AdresseID;
                    }                                        
                    
                    if(aContact!=null)
                    {
                        newCustomer.Kontakt = aContact;
                        newCustomer.KontaktID = aContact.KontaktID;
                    }                    

                    list.addToCustomerList(newCustomer);

                    loadComboBoxAddress();
                    loadComboBoxContact();
                    clearComponents();
                }
            }
            else
            {
                MessageBox.Show("Bitte füllen Sie die notwendigen Felder aus.");
            }
        }

        private void loadComboBoxAddress()
        {
            placeHolderAddress = list.AddressList.FirstOrDefault(address => address.Ort == "Neue Adresse");

            if (placeHolderAddress==null)
            {
                placeHolderAddress = new Adresse { Ort = "Neue Adresse" };                
                list.addToAddressList((placeHolderAddress));

                comboBoxAddress.Items.Clear();
                foreach(Adresse address in list.AddressList)
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

        private void loadComboBoxContact()
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

        private void enableAddressComponents(bool status)
        {
            textBoxStreet.IsEnabled = status;
            textBoxHouseNumber.IsEnabled = status;
            textBoxZipCode.IsEnabled = status;
            textBoxCity.IsEnabled = status;
        }

        private void enableContactComponents(bool status)
        {
            textBoxMail.IsEnabled = status;
            textBoxPhoneNumber.IsEnabled = status;
            textBoxMobileNumber.IsEnabled = status;
            textBoxFaxNumber.IsEnabled = status;
        }

        private void clearComponents()
        {
            textBoxCustomerNumber.Text = null;
            textBoxFirstName.Text = null;
            textBoxLastName.Text = null;
            textBoxGender.Text = null;
            textBoxAge.Text = null;
        }

        private void textBoxAge_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(e.Text == "." && isTextInputTypeOfInteger(e.Text)==false)
            {
                e.Handled = true;
            }
            else if(e.Text == ".")
            {
                if(((TextBox)sender).Text.IndexOf(e.Text) > -1)
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
    }
}