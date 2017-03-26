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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental
{ 
    public partial class MainWindow : Window
    {
        #region Variables

        private String usertype;
        private String username;
        private int signInId;
        private int usertypeId;

        private DM_DBConnection databaseConnection;
        private CL_List listobject;
        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            Initialize();            
        }
        #endregion

        #region Logic

        private void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            listobject = CL_List.Instance;
        }

        private void VerifyLogin(String id, String pw)
        {
            bool verify = false;
            foreach (Anmeldung aSignIn in listobject.LoginList)
            {
                if (aSignIn.Benutzername.Equals(id) && aSignIn.Passwort.Equals(pw))
                {
                    verify = true;
                    signInId = aSignIn.AnmeldungID;
                    foreach (Benutzer aUser in listobject.UserList)
                    {
                        if (aUser.AnmeldeID.Equals(signInId))
                        {
                            usertypeId = Convert.ToInt32(aUser.BenutzerartID);
                            username = aUser.Benutzernamen;
                            foreach (Benutzerart aUsertype in listobject.UserTypeList)
                            {
                                if (aUsertype.BenutzerartID.Equals(usertypeId))
                                {
                                    usertype = aUsertype.Bezeichnung;
                                    
                                }
                            }
                        }
                    }
                    MessageBox.Show("Login erfolgreich. Rechte: " + usertype);
                    new GUI_MainMenu(usertype, username).Show();
                    this.Close();
                }
             }

            if (verify == false)
            {
                MessageBox.Show("Daten falsch. Erneute Eingabe erforderlich.");
            }
        }
        #endregion

        #region Events

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            VerifyLogin(textBoxUsername.Text, passwordBoxPassword.Password);            
        }
        #endregion
    }
}