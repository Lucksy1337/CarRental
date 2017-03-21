using CarRental.CarRentalServiceReference;
//using CarRental.CarRentalSchoolServiceReference;
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
    /// <summary>
    /// Interaction logic for GUI_AccountManagement.xaml
    /// </summary>
    public partial class GUI_AccountManagement : Window
    {
        private DM_DBConnection databaseConnection;
        private CL_List listobject;
        private String aktiverNutzer;

        public GUI_AccountManagement()
        {
            InitializeComponent();
        }

        public GUI_AccountManagement(String username)
        {
            InitializeComponent();
            initialize(username);
        }

        private void initialize(String username)
        {
            databaseConnection = DM_DBConnection.Instance;
            listobject = CL_List.Instance;
            aktiverNutzer = username;
            labelActiveUser.Content = aktiverNutzer;
        }

        private void changePassword()
        {
            if(listobject.UserList != null)
            {
                Anmeldung aLogin = new Anmeldung();
                Benutzer aUser = new Benutzer();

                foreach (Benutzer User in listobject.UserList)
                {
                    if (aUser.Benutzernamen.Equals(aktiverNutzer))
                    {
                        aUser = User;
                        aLogin = User.Anmeldung;
                        break;
                    }
                }

                aLogin.Passwort = passwordBoxNewPassword.Password;

                var result = MessageBox.Show("Möchten Sie die Kundendaten speichern?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {

                    Anmeldung oldLogin = listobject.LoginList.Where(login => login.AnmeldungID.Equals(aUser.AnmeldeID)).First();
                    listobject.LoginList.Remove(oldLogin);
                    listobject.addToLoginList(aLogin);
                }

                clearFields();
                MessageBox.Show("Passwort erfolgreich abgeändert.");
            }
        }

        private void verifyInput()
        {
            Boolean valid = false;

            if (passwordBoxOldPassword.Password.Contains(" ") || passwordBoxNewPassword.Password.Contains(" "))
            {
                MessageBox.Show("In Ihren Eingaben dürfen keine Leerzeichen enthalten sein.");
            }
            else
            {
                if (passwordBoxOldPassword.Password.Equals("") || passwordBoxOldPassword.Password.Equals(""))
                {
                    MessageBox.Show("Füllen Sie bitte zuerst alle Felder aus.");
                }
                else
                {

                    if (passwordBoxNewPassword.Password.Length < 6)
                    {
                        MessageBox.Show("Das Password muss mindestens 6 Zeichen lang sein.");
                    }
                    else
                    {

                        if (passwordBoxOldPassword.Password.Equals(passwordBoxNewPassword.Password))
                        {
                            MessageBox.Show("Ihr neues Passwort darf nicht dem alten Passwort entsprechen.");
                            valid = false;
                        }
                        else
                        {
                            valid = true;
                        }

                    }

                }
            }

            
            if (valid == true)
            {
                changePassword();
            }
        }

        private void clearFields()
        {
            passwordBoxNewPassword.Password = null;
            passwordBoxOldPassword.Password = null;
        }

        private void buttonConfirmNewPassword_Click(object sender, RoutedEventArgs e)
        {
            verifyInput();
        }
    }
}
