//using CarRental.CarRentalServiceReference;
using CarRental.CarRentalSchoolServiceReference;
//using CarRental.CarRentalEbertsonServiceReference;
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
    public partial class GUI_UserManagement : Window
    {
        private DM_DBConnection databaseConnection;
        private CL_List listobject;
        private String activeUser;

        public GUI_UserManagement()
        {
            InitializeComponent();            
        }

        public GUI_UserManagement(String username)
        {
            InitializeComponent();
            Initialize(username);
        }

        private void Initialize(String username)
        {
            databaseConnection = DM_DBConnection.Instance;
            listobject = CL_List.Instance;
            activeUser = username;

            if (listobject.UserTypeList != null)
            {
                foreach (Benutzerart aUserType in listobject.UserTypeList)
                {
                    comboBoxUserType.Items.Add(aUserType.Bezeichnung);
                }
            }

            listBoxRegistratedUsers.DisplayMemberPath = "Benutzernamen";
            listBoxRegistratedUsers.ItemsSource = listobject.UserList;
            listBoxRegistratedUsers.SelectedIndex = 0;
        }

        private void verifyInput()
        {
            Boolean valid = false;

            if(textBoxId.Text.Contains(" ") || passwordBoxPassword.Password.Contains(" ") || textBoxUsername.Text.Contains(" "))
            {
                MessageBox.Show("In Ihren Eingaben dürfen keine Leerzeichen enthalten sein.");              
            }
            else
            {
                if (textBoxId.Text.Equals("") || textBoxUsername.Text.Equals("") || passwordBoxPassword.Password.Equals("")) //was hast du mit der textBoxId vor? :p^^
                {
                    MessageBox.Show("Füllen Sie bitte zuerst alle Felder aus.");
                }
                else
                {
                    if (comboBoxUserType.SelectedIndex == -1)
                    {
                        MessageBox.Show("Geben Sie eine Benutzerart an.");
                    }
                    else
                    {
                        if (passwordBoxPassword.Password.Length < 6)
                        {
                            MessageBox.Show("Das Password muss mindestens 6 Zeichen lang sein.");
                        }
                        else
                        {
                            foreach (Benutzer aUser in listobject.UserList)
                            {
                                if (aUser.Benutzernamen == textBoxUsername.Text)
                                {
                                    MessageBox.Show("Benutzername bereits vorhanden. Ihr Benutzername muss individuell sein.");
                                    valid = false;
                                    break;
                                }
                                else
                                {
                                    valid = true;
                                }
                            }
                        }
                    }
                }
            }

           
            if (valid == true)
            {
                createUser();
            }
        }

        private void createUser()
        {
            var result = MessageBox.Show("Möchten Sie einen neuen Benutzer anlegen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Anmeldung newLogin = new Anmeldung();
                Benutzer newUser = new Benutzer();
                Benutzerart newUsertype = new Benutzerart();

                //Anmeldung
                newLogin.AnmeldungID = 0;
                newLogin.Benutzername = textBoxId.Text;
                newLogin.Passwort = passwordBoxPassword.Password;
                listobject.addToLoginList(newLogin);

                //Benutzerart
                if (listobject.UserTypeList != null)
                {
                    foreach (Benutzerart aUserType in listobject.UserTypeList)
                    {
                        if (comboBoxUserType.SelectedValue.Equals(aUserType.Bezeichnung))
                        {
                            newUsertype = aUserType;
                        }
                    }
                }

                //Benutzer
                newUser.BenutzerID = 0;
                newUser.Benutzernamen = textBoxUsername.Text;

                if (newLogin != null)
                {
                    newUser.Anmeldung = newLogin;
                    newUser.AnmeldeID = newLogin.AnmeldungID;
                }

                if (newUsertype != null)
                {
                    newUser.Benutzerart = newUsertype;
                    newUser.BenutzerartID = newUsertype.BenutzerartID;
                }

                listobject.addToUserList(newUser);
                clearFields();

                if (listBoxRegistratedUsers.Items.Count > 0)
                {
                    buttonDelete.IsEnabled = true;
                }
                MessageBox.Show("Benutzer erfolgreich angelegt.");

            }
        }

        private void exterminateUser()
        {
            var result = MessageBox.Show("Möchten Sie den ausgewählten Benutzer wirklich löschen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Anmeldung deletingLogin;
                Benutzer deletingUser;
                if (listobject.UserList != null && listobject.LoginList != null)
                {
                   // deletingUser = listobject.UserList.ElementAt(listBoxRegistratedUsers.SelectedIndex);
                    deletingUser = (Benutzer)listBoxRegistratedUsers.SelectedItem;

                    if(deletingUser.Benutzernamen != activeUser)
                    {
                        deletingLogin = listobject.LoginList.Where(login => login.AnmeldungID.Equals(deletingUser.AnmeldeID)).First();
                        
                        listobject.UserList.Remove(deletingUser);
                        listobject.LoginList.Remove(deletingLogin);

                        if (listBoxRegistratedUsers.Items.Count == 0)
                        {
                            buttonDelete.IsEnabled = false;
                        }
                        else
                        {
                            listBoxRegistratedUsers.SelectedItem = 0;
                        }
                        MessageBox.Show("Benutzer erfolgreich gelöscht.");
                    }
                    else
                    {
                        MessageBox.Show("Sie können sich nicht selber löschen.");
                    }                   
                }
                else
                {
                    MessageBox.Show("Fehler beim Löschen des Benutzers");
                }
                
            }
        }

        private void clearFields()
        {
            comboBoxUserType.SelectedIndex = -1;
            textBoxId.Text = null;
            textBoxUsername.Text = null;
            passwordBoxPassword.Password = null;

        }


        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            verifyInput();
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            exterminateUser();
        }
    }
}
