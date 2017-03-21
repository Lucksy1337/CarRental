//using CarRental.CarRentalServiceReference;
using CarRental.CarRentalSchoolServiceReference;
//using CarRental.CarRentalEbertsonServiceReference;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class GUI_OrderManagement : Window
    {
        private DM_DBConnection databaseConnection;
        private CL_List list;
        TimeSpan orderTimeSpan;

        public GUI_OrderManagement()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;            
            loadComboBoxVehicleType();
            loadComboBoxCustomer();
        }

        private void loadComboBoxVehicleType()
        {
            comboBoxVehicleType.Items.Clear();
            foreach (Fahrzeugtyp aVehicleType in list.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(aVehicleType);
            }
        }

        private void loadComboBoxCustomer()
        {
            comboBoxCustomer.Items.Clear();
            foreach (Kunde customer in list.CustomerList)
            {
                comboBoxCustomer.Items.Add(customer);
            }
        }

        private void loadListBoxCreatedVehicles()
        {
            Fahrzeugtyp aVehicleType = (Fahrzeugtyp)comboBoxVehicleType.SelectedItem;

            if (aVehicleType != null)
            {
                list.VehicleSortedByTypeList.Clear();
                listBoxVehicleInformation.Items.Clear();
                foreach (Fahrzeug vehicle in list.VehicleList)
                {
                    if (aVehicleType.Equals(vehicle.Fahrzeugtyp) && vehicle.Verfügbar)
                    {
                        list.addToVehicleSortedByTypeList(vehicle);
                    }
                }

                if(list.VehicleSortedByTypeList.Count != 0)
                {
                    foreach (Fahrzeug vehicle in list.VehicleSortedByTypeList)
                    {
                        listBoxVehicleInformation.Items.Add(vehicle);
                    }

                    enableOrderComponents(true);
                    listBoxVehicleInformation.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Für diesen Fahrzeugtyp sind momentan keine Fahrzeuge verfügbar.");
                    listBoxVehicleInformation.Items.Clear();                    
                    enableOrderComponents(false);
                }                
            }
        }

        private void comboBoxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadListBoxCreatedVehicles();            
        }

        private void buttonBookVehicle_Click(object sender, RoutedEventArgs e)
        {
            Auftrag newOrder = null;           
            bool textWrongInput = false;

            if (comboBoxVehicleType.SelectedItem != null && comboBoxCustomer.SelectedItem != null &&
                datePickerOrderDate.SelectedDate != null && datePickerReturnDate.SelectedDate != null)
            {
                TimeSpan orderTimeSpan = Convert.ToDateTime(datePickerReturnDate.SelectedDate) - Convert.ToDateTime(datePickerOrderDate.SelectedDate);

                newOrder = new Auftrag();
                newOrder.AuftragID = 0;
                newOrder.Kunde = (Kunde)comboBoxCustomer.SelectedItem;
                newOrder.KundeID = newOrder.Kunde.KundeID;
                newOrder.Fahrzeug = (Fahrzeug)listBoxVehicleInformation.SelectedItem;
                newOrder.FahrzeugID = newOrder.Fahrzeug.FahrzeugID;
                newOrder.Fahrzeug.Verfügbar = false;
                newOrder.Auftragsdatum = Convert.ToDateTime(datePickerOrderDate.SelectedDate);
                newOrder.Rückgabedatum = Convert.ToDateTime(datePickerReturnDate.SelectedDate);
                newOrder.Gesamtpreis = double.Parse(textBoxTotalPrice.Text, NumberStyles.Currency);
            }
            else
            {
                textWrongInput = true;
                MessageBox.Show("Bitte füllen Sie die Auftragsdaten korrekt aus.");
            }
           

            if (!textWrongInput)
            {
                var result = MessageBox.Show("Möchten Sie das Fahrzeug buchen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    clearComponents();
                    list.addToOrderList(newOrder);
                    loadListBoxCreatedVehicles();
                }
            }
        }

        private void datePickerOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (datePickerReturnDate.SelectedDate != null)
            {
                calculateOrderData();                
            }
        }

        private void datePickerReturnDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerOrderDate.SelectedDate != null)
            {
                calculateOrderData();
            }
        }

        private void calculateOrderData()
        {
            orderTimeSpan = Convert.ToDateTime(datePickerReturnDate.SelectedDate) - Convert.ToDateTime(datePickerOrderDate.SelectedDate);
            textBoxPeriotOfTime.Text = orderTimeSpan.Days.ToString();
            textBoxTotalPrice.Text = (Convert.ToInt32(textBoxPeriotOfTime.Text) * ((Fahrzeug)listBoxVehicleInformation.SelectedItem).MietpreisProTag).ToString("C");            
        }

        private void clearComponents()
        {        
            comboBoxCustomer.SelectedItem = null;
            datePickerOrderDate.SelectedDate = null;
            datePickerReturnDate.SelectedDate = null;
            textBoxPeriotOfTime.Text = null;
            textBoxTotalPrice.Text = null;
        }

        private void enableOrderComponents(bool status)
        {
            datePickerOrderDate.IsEnabled = status;
            datePickerReturnDate.IsEnabled = status;
        }
    }    
}
