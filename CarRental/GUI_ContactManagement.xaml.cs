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
    /// Interaktionslogik für GUI_ContactCreation.xaml
    /// </summary>
    public partial class GUI_ContactManagement : Window
    {
        DM_DBConnection databaseConnection;
        CL_List list;

        public GUI_ContactManagement()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;            
        }

        private void buttonCreateContact_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
