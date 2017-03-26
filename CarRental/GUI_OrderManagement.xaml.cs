using CarRental.CarRentalSchoolServiceReference;
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
        #region Variables
        private DM_DBConnection databaseConnection;
        private CL_List list;
        TimeSpan orderTimeSpan;
        bool wrongTimeSpan;
        #endregion

        #region Constructor

        public GUI_OrderManagement()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Logic

        private void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            wrongTimeSpan = false;            
            LoadComboBoxVehicleType();
            LoadComboBoxCustomer();
        }

        private void LoadComboBoxVehicleType()
        {
            comboBoxVehicleType.Items.Clear();
            foreach (Fahrzeugtyp aVehicleType in list.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(aVehicleType);
            }
        }

        private void LoadComboBoxCustomer()
        {
            comboBoxCustomer.Items.Clear();
            foreach (Kunde customer in list.CustomerList)
            {
                comboBoxCustomer.Items.Add(customer);
            }
        }       

        private void BookVehicle()
        {
            Auftrag newOrder = null;
            bool textWrongInput = false;

            if (comboBoxVehicleType.SelectedItem != null && comboBoxCustomer.SelectedItem != null && datePickerOrderDate.SelectedDate != null &&
                datePickerReturnDate.SelectedDate != null && !textBoxPeriotOfTime.Text.Equals("") && !textBoxTotalPrice.Text.Equals("") &&
                Convert.ToDateTime(datePickerOrderDate.SelectedDate) >= DateTime.Today && Convert.ToDateTime(datePickerReturnDate.SelectedDate) > DateTime.Today)
            {
                TimeSpan orderTimeSpan = Convert.ToDateTime(datePickerReturnDate.SelectedDate) - Convert.ToDateTime(datePickerOrderDate.SelectedDate);

                Kunde aCustomer = (Kunde)comboBoxCustomer.SelectedItem;
                Fahrzeug aVehicle = (Fahrzeug)listBoxVehicleInformation.SelectedItem;

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
            }

            if (textWrongInput && wrongTimeSpan)
            {
                MessageBox.Show("Bitte füllen Sie die Auftragsdaten korrekt aus." + Environment.NewLine +
                                Environment.NewLine + "     Der ausgewählte Zeitraum ist ungültig.");
            }
            else if (textWrongInput)
            {
                MessageBox.Show("Bitte füllen Sie die Auftragsdaten korrekt aus.");
            }

            if (!textWrongInput && !wrongTimeSpan)
            {
                var result = MessageBox.Show("Möchten Sie das Fahrzeug buchen?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ClearComponents();
                    list.addToOrderList(newOrder);
                    LoadListBoxCreatedVehicles();
                }
            }
        }

        private void LoadListBoxCreatedVehicles()
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

                if (list.VehicleSortedByTypeList.Count != 0)
                {
                    foreach (Fahrzeug vehicle in list.VehicleSortedByTypeList)
                    {
                        listBoxVehicleInformation.Items.Add(vehicle);
                    }

                    EnableOrderComponents(true);
                    listBoxVehicleInformation.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Für diesen Fahrzeugtyp sind momentan keine Fahrzeuge verfügbar.");
                    listBoxVehicleInformation.Items.Clear();
                    EnableOrderComponents(false);
                }
            }
        }

        private void CalculateOrderData()
        {
            orderTimeSpan = Convert.ToDateTime(datePickerReturnDate.SelectedDate) - Convert.ToDateTime(datePickerOrderDate.SelectedDate);

            if (Convert.ToDateTime(datePickerOrderDate.SelectedDate) >= DateTime.Today &&
                Convert.ToDateTime(datePickerReturnDate.SelectedDate) > DateTime.Today)
            {
                textBoxPeriotOfTime.Text = orderTimeSpan.Days.ToString();
                textBoxTotalPrice.Text = (Convert.ToInt32(textBoxPeriotOfTime.Text) * ((Fahrzeug)listBoxVehicleInformation.SelectedItem).MietpreisProTag).ToString("C");
                wrongTimeSpan = false;
            }
            else
            {
                textBoxPeriotOfTime.Text = null;
                textBoxTotalPrice.Text = null;
                wrongTimeSpan = true;              
            }               
        }

        private void ClearComponents()
        {        
            comboBoxCustomer.SelectedItem = null;
            datePickerOrderDate.SelectedDate = null;
            datePickerReturnDate.SelectedDate = null;
            textBoxPeriotOfTime.Text = null;
            textBoxTotalPrice.Text = null;
        }

        private void EnableOrderComponents(bool status)
        {
            datePickerOrderDate.IsEnabled = status;
            datePickerReturnDate.IsEnabled = status;
        }
        #endregion

        #region Events   

        private void ButtonBookVehicle_Click(object sender, RoutedEventArgs e)
        {
            BookVehicle();
        }

        private void ComboBoxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadListBoxCreatedVehicles();
        }

        private void DatePickerOrderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerReturnDate.SelectedDate != null)
            {
                CalculateOrderData();
            }
        }

        private void DatePickerReturnDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerOrderDate.SelectedDate != null)
            {
                CalculateOrderData();
            }
        }

        private void listBoxVehicleInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToDateTime(datePickerOrderDate.SelectedDate) >= DateTime.Today &&
                Convert.ToDateTime(datePickerReturnDate.SelectedDate) > DateTime.Today)
            {
                CalculateOrderData();
            }
        }
        #endregion        
    }
}