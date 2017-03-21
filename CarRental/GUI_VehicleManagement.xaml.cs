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
    public partial class GUI_VehicleManagement : Window
    {
        private DM_DBConnection databaseConnection;
        private CL_List list;
        
        public GUI_VehicleManagement()
        {
            InitializeComponent();
            Initialize();          
        }

        private void Initialize()
        {
            databaseConnection = DM_DBConnection.Instance;
            list = CL_List.Instance;
            loadComboBoxVehicleType();
            loadComboBoxInsurancePackage();
        }
        
        private void loadComboBoxVehicleType()
        {
            comboBoxVehicleType.Items.Clear();
            foreach (Fahrzeugtyp aVehicleType in list.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(aVehicleType);
            }                        
        }

        private void loadComboBoxInsurancePackage()
        {
            comboBoxInsurancePackage.Items.Clear();
            foreach (Versicherungspaket aInsurancePackage in list.InsurancePackageList)
            {
                comboBoxInsurancePackage.Items.Add(aInsurancePackage);
            }
        }

        private void loadListBoxCreatedVehicles()
        {
            Fahrzeugtyp aVehicleType = (Fahrzeugtyp)comboBoxVehicleType.SelectedItem;

            if (aVehicleType != null)
            {
                list.VehicleSortedByTypeList.Clear();
                listBoxCreatedVehicles.Items.Clear();
                foreach (Fahrzeug vehicle in list.VehicleList)
                {
                    if (aVehicleType.Equals(vehicle.Fahrzeugtyp))
                    {
                        list.addToVehicleSortedByTypeList(vehicle);
                    }
                }

                foreach (Fahrzeug vehicle in list.VehicleSortedByTypeList)
                {
                    listBoxCreatedVehicles.Items.Add(vehicle);
                }
            }

        }

        private void comboBoxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            loadListBoxCreatedVehicles();
        }      

        private void listBoxCreatedVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextBoxes((Fahrzeug)listBoxCreatedVehicles.SelectedItem);
        }        

        private void buttonNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            enableVehicleComponents(true);
            buttonDeleteVehicle.IsEnabled = false;
            buttonModifyVehicle.IsEnabled = false;
            buttonCreateVehicle.IsEnabled = true;
            clearComponents();
        }

        private void buttonModifyVehicle_Click(object sender, RoutedEventArgs e)
        {
            Fahrzeug aVehicle = (Fahrzeug)listBoxCreatedVehicles.SelectedItem;
            aVehicle.Bezeichnung = textBoxDescription.Text;
            aVehicle.Marke = textBoxBrand.Text;
            aVehicle.Baujahr = Convert.ToInt32(textBoxVintage.Text);
            aVehicle.Kilometerstand = Convert.ToDouble(textBoxMileage.Text);
            aVehicle.Schaltung = textBoxGearChange.Text;
            aVehicle.Sitze = Convert.ToInt32(textBoxSeats.Text);
            aVehicle.Türe = Convert.ToInt32(textDoors.Text);
            aVehicle.Naviagationssystem = Convert.ToBoolean(checkBoxNavigation.IsChecked);
            aVehicle.Klimaanlage = Convert.ToBoolean(checkBoxAirConditioning.IsChecked);
            aVehicle.MietpreisProTag = Convert.ToDouble(textBoxRentPerDay.Text);
            aVehicle.Verfügbar = Convert.ToBoolean(checkBoxAvailability.IsChecked);
            list.addToVehicleList(aVehicle);
            MessageBox.Show("Fahrzeug verändert!");
        }

        private void buttonCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            Fahrzeugtyp aVehicleType = (Fahrzeugtyp)comboBoxVehicleType.SelectedItem;
            Versicherungspaket aInsurancePackage = (Versicherungspaket)comboBoxInsurancePackage.SelectedItem;

            Fahrzeug newVehicle = new Fahrzeug();
            newVehicle.FahrzeugID = 0;
            newVehicle.Fahrzeugtyp = aVehicleType;
            newVehicle.FahrzeugtypID = aVehicleType.FahrzeugtypID;
            newVehicle.Versicherungspaket = aInsurancePackage;
            newVehicle.VersicherungspaketID = aInsurancePackage.VersicherungspaketID;
            newVehicle.Bezeichnung = textBoxDescription.Text;
            newVehicle.Marke = textBoxBrand.Text;
            newVehicle.Baujahr = Convert.ToInt32(textBoxVintage.Text);
            newVehicle.Kilometerstand = Convert.ToDouble(textBoxMileage.Text);
            newVehicle.Schaltung = textBoxGearChange.Text;
            newVehicle.Sitze = Convert.ToInt32(textBoxSeats.Text);
            newVehicle.Türe = Convert.ToInt32(textDoors.Text);
            newVehicle.Naviagationssystem = Convert.ToBoolean(checkBoxNavigation.IsChecked);
            newVehicle.Klimaanlage = Convert.ToBoolean(checkBoxAirConditioning.IsChecked);
            newVehicle.MietpreisProTag = Convert.ToDouble(textBoxRentPerDay.Text);
            newVehicle.Verfügbar = Convert.ToBoolean(checkBoxAvailability.IsChecked);

            clearComponents();
            list.addToVehicleList(newVehicle);
            loadListBoxCreatedVehicles();
        }

        public void updateTextBoxes(Fahrzeug selectedVehicle)
        {            
            bool cancelVehicleCreation = false;

            if (buttonCreateVehicle.IsEnabled && listBoxCreatedVehicles.SelectedItem != null)
            {
                var result = MessageBox.Show("Möchten Sie die Fahrzeugerstellung verlassen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    cancelVehicleCreation = true;
                }                
            }

            if (selectedVehicle != null)
            {
                if (cancelVehicleCreation || !buttonCreateVehicle.IsEnabled)
                {
                    comboBoxInsurancePackage.SelectedItem = selectedVehicle.Versicherungspaket;
                    textBoxDescription.Text = selectedVehicle.Bezeichnung;
                    textBoxBrand.Text = selectedVehicle.Marke;
                    textBoxVintage.Text = Convert.ToString(selectedVehicle.Baujahr);
                    textBoxMileage.Text = Convert.ToString(selectedVehicle.Kilometerstand);
                    textBoxGearChange.Text = selectedVehicle.Schaltung;
                    textBoxSeats.Text = Convert.ToString(selectedVehicle.Sitze);
                    textDoors.Text = Convert.ToString(selectedVehicle.Türe);
                    checkBoxNavigation.IsChecked = Convert.ToBoolean(selectedVehicle.Naviagationssystem);
                    checkBoxAirConditioning.IsChecked = Convert.ToBoolean(selectedVehicle.Klimaanlage);
                    textBoxRentPerDay.Text = Convert.ToString(selectedVehicle.MietpreisProTag);
                    checkBoxAvailability.IsChecked = Convert.ToBoolean(selectedVehicle.Verfügbar);

                    enableVehicleComponents(true);
                    buttonDeleteVehicle.IsEnabled = true;
                    buttonModifyVehicle.IsEnabled = true;
                    buttonCreateVehicle.IsEnabled = false;
                }
                else
                {
                    listBoxCreatedVehicles.SelectedItem = null;
                }
            }
        }

        private void enableVehicleComponents(bool status)
        {
            comboBoxVehicleType.IsEnabled = status;
            comboBoxInsurancePackage.IsEnabled = status;
            textBoxDescription.IsEnabled = status;
            textBoxBrand.IsEnabled = status;
            textBoxVintage.IsEnabled = status;
            textBoxMileage.IsEnabled = status;
            textBoxGearChange.IsEnabled = status;
            textBoxSeats.IsEnabled = status;
            textDoors.IsEnabled = status;
            checkBoxNavigation.IsEnabled = status;
            checkBoxAirConditioning.IsEnabled = status;
            textBoxRentPerDay.IsEnabled = status;
            checkBoxAvailability.IsEnabled = status;                  
        }        

        private void clearComponents()
        {
            listBoxCreatedVehicles.Items.Clear();
            comboBoxVehicleType.SelectedItem = null;
            comboBoxInsurancePackage.SelectedItem = null;
            textBoxDescription.Text = null;
            textBoxBrand.Text = null;
            textBoxVintage.Text = null;
            textBoxMileage.Text = null;
            textBoxGearChange.Text = null;
            textBoxSeats.Text = null;
            textDoors.Text = null;
            checkBoxNavigation.IsChecked = false;
            checkBoxAirConditioning.IsChecked = false;
            textBoxRentPerDay.Text = null;
            checkBoxAvailability.IsChecked = false;            
        }        
    }
}
