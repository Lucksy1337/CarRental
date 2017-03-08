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
    /// <summary>
    /// Interaktionslogik für GUI_CustomerManagement.xaml
    /// </summary>
    public partial class GUI_CustomerManagement : Window
    {
        DM_DBConnection con = DM_DBConnection.Instance;
        CL_List list = CL_List.Instance;
        public GUI_CustomerManagement()
        {
            InitializeComponent();
            test();
        }

        public void test()
        {
            Adresse aAddress = list.AddressList.First();
            textBoxAge.Text = aAddress.PLZ.ToString();
        }
    }
}

