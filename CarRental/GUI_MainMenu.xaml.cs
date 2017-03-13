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
    /// Interaktionslogik für GUI_MainMenu.xaml
    /// </summary>
    public partial class GUI_MainMenu : Window
    {

        public GUI_MainMenu()
        {
            InitializeComponent();
        }

        public GUI_MainMenu(String usertype, String username)
        {
            InitializeComponent();
            setGuiValues(usertype, username);

        }

        private void setGuiValues(String usertype, String username)
        {
            labelActiveUsername.Content = username;
            labelActiveUsertype.Content = usertype;

            switch (usertype)
            {
                 case "Admin":
                    buttonUserManagement.IsEnabled = true;
                    buttonVehicleManagement.IsEnabled = true;
                    break;                                        
            }


               
        }

        private void buttonCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_CustomerManagement().Show();
        }

        private void buttonOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_OrderManagement().Show();
        }

        private void buttonUserManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_UserManagement().Show();
        }

        private void buttonVehicleManagement_Click(object sender, RoutedEventArgs e)
        {
            new GUI_VehicleManagement().Show();
        }

        private void buttonSaveToDb_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
