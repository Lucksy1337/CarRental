using CarRental.CarRentalServiceReference;
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
    public partial class GUI_VehicleManagement : Window
    {
        DM_DBConnection dbconnect = DM_DBConnection.Instance;
        CL_List aList = CL_List.Instance;

        public GUI_VehicleManagement()
        {
            InitializeComponent();
            fillComboBox();
            //comboBoxVehicleType.Items.Add(aList.VehicleTypeList);
            //comboBoxInsurancePackage.Items.Add(aList.InsurancePackageList);
        }
        
        public void fillComboBox()
        {
            foreach (Fahrzeugtyp aVehicleType in aList.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(aVehicleType);
            }
        }
        
        public void fahrzeugErstellen(object sender, RoutedEventArgs e)
        {
            //dbconnect.addVehicle();
            
        }
        private void fahrzeugAendern(object sender, RoutedEventArgs e)
        {
            //dbconnect.updateVehicle();
        }
        private void fahrzeugLoeschen(object sender, RoutedEventArgs e)
        {
            //dbconnect.deleteVehicle();
        }
    }
}
