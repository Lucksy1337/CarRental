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
    public partial class GUI_AccountManagement : Window
    {
        #region Variables
        private DM_DBConnection databaseConnection;
        private CL_List listobject;
        private String activeUser;
        #endregion

        #region Constructor
        public GUI_AccountManagement()
        {
            InitializeComponent();
        }

        public GUI_AccountManagement(String username)
        {
            InitializeComponent();
            Initialize(username);
        }
        #endregion

        #region Logic

        private void Initialize(String username)
        {
            databaseConnection = DM_DBConnection.Instance;
            listobject = CL_List.Instance;
            activeUser = username;
            labelActiveUser.Content = activeUser;
        }
        
        private void ChangePassword()
        {
            if(listobject.UserList != null)
            {
                Anmeldung aLogin = new Anmeldung();
                Benutzer aUser = new Benutzer();

                if(listobject.UserList != null || listobject.LoginList != null)
                {
                    aUser = listobject.UserList.Where(user => user.Benutzernamen.Equals(activeUser)).First(); ;
                    aLogin = listobject.LoginList.Where(login => login.AnmeldungID.Equals(aUser.AnmeldeID)).First();

                    if(aLogin.Passwort.Equals(passwordBoxOldPassword.Password))
                    {
                        aLogin.Passwort = passwordBoxNewPassword.Password;

                        var result = MessageBox.Show("Möchten Sie die Kundendaten speichern?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {

                            Anmeldung oldLogin = listobject.LoginList.Where(login => login.AnmeldungID.Equals(aUser.AnmeldeID)).First();
                            listobject.LoginList.Remove(oldLogin);
                            listobject.addToLoginList(aLogin);
                        }

                        ClearFields();
                        MessageBox.Show("Passwort erfolgreich abgeändert.");
                    }
                    else
                    {
                        MessageBox.Show("Das von Ihnen angegebene alte Passwort entspricht nicht ihrem aktuellen Passwort.");
                    }
                }
                else
                {
                    MessageBox.Show("Fehler beim überprüfen der Benutzerdaten");
                }               
                
            }
        }

        private void VerifyInput()
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
                ChangePassword();
            }
        } 

        private void ClearFields()
        {
            passwordBoxNewPassword.Password = null;
            passwordBoxOldPassword.Password = null;
        }
        #endregion

        #region Events
        private void ButtonConfirmNewPassword_Click(object sender, RoutedEventArgs e)
        {
            VerifyInput();
        }
        #endregion
    }
}
