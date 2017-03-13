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
    /// <summary>
    /// Interaktionslogik für GUI_UserManagement.xaml
    /// </summary>
    public partial class GUI_UserManagement : Window
    {
        DM_DBConnection databaseConnection;
        private CL_List listobject;
        public GUI_UserManagement()
        {
            InitializeComponent();
            databaseConnection = DM_DBConnection.Instance;
            listobject = CL_List.Instance;
            initialize();
            
        }

        private void initialize()
        {
            if(listobject.UserTypeList != null)
            {
                foreach (Benutzerart aUserType in listobject.UserTypeList)
                {
                    comboBoxUserType.Items.Add(aUserType.Bezeichnung);
                }
            }
            if(listobject.UserList != null)
            {
                foreach (Benutzer aUser in listobject.UserList)
                {
                    comboBoxUserType.Items.Add(aUser.Benutzernamen);
                }
            }          
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxUsername.Equals("") || passwordBoxPassword.Password.Equals(""))
            {
                MessageBox.Show("Füllen Sie bitte zuerst alle Felder aus.");
            }
            else
            {
                if(passwordBoxPassword.Password.Length<6)
                {
                    MessageBox.Show("Das Password muss mindestens 6 Zeichen lang sein");                    
                }
                else
                {

                }
            }
            
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
