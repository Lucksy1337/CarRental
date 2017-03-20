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
    public partial class GUI_VehicleManagement : Window
    {
        DM_DBConnection dbconnect = DM_DBConnection.Instance;
        CL_List aList = CL_List.Instance;
        Fahrzeug aVehicle;
        //Fahrzeugtyp aFahrzeugtyp;
        
        public GUI_VehicleManagement()
        {
            InitializeComponent();
            fillComboBox();
        }
        
        public void fillComboBox()
        {
            foreach (Fahrzeugtyp aVehicleType in aList.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(aVehicleType);
            }
            foreach (Versicherungspaket aInsurancePackage in aList.InsurancePackageList)
            {
                comboBoxInsurancePackage.Items.Add(aInsurancePackage);
            }
        }
        public void updateListBox()
        {
            Fahrzeugtyp aVehicleType = (Fahrzeugtyp)comboBoxVehicleType.SelectedItem;
            //aList.addToVehicleSortedByTypeList(aFahrzeug);
            listBoxCreatedVehicles.Items.Clear();
            foreach (Fahrzeug aVehicle in aList.VehicleList)
            {
                if (aVehicleType.Equals(aVehicle.Fahrzeugtyp))
                {
                    aList.addToVehicleSortedByTypeList(aVehicle);
                }
            }
            foreach (Fahrzeug aVehicle in aList.VehicleSortedByTypeList)
            {
                listBoxCreatedVehicles.Items.Add(aVehicle);
            }
        }
        public void updateTextBoxes(Fahrzeug selectedVehicle)
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

        }


        private void comboBoxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateListBox();
        }

        private void comboBoxInsurancePackage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listBoxCreatedVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTextBoxes((Fahrzeug)listBoxCreatedVehicles.SelectedItem);
        }

        private void newVehicle(object sender, RoutedEventArgs e)
        {
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

            buttonCreateVehicle.IsEnabled = true;

            MessageBox.Show("Bitte Daten angeben");
        }

        private void fahrzeugErstellen_Click(object sender, RoutedEventArgs e)
        {
            aVehicle = new Fahrzeug();
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
            aList.addToVehicleList(aVehicle);
            updateListBox();
            MessageBox.Show("Fahrzeug erstellt!");
            buttonCreateVehicle.IsEnabled = false;
        }

        private void fahrzeugAendern_Click(object sender, RoutedEventArgs e)
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
            aList.addToVehicleList(aVehicle);

    }

        private void fahrzeugLoeschen_Click(object sender, RoutedEventArgs e)
        {
            Fahrzeug aVehicle = (Fahrzeug)listBoxCreatedVehicles.SelectedItem;
            aList.VehicleList.Remove(aVehicle);
            updateListBox();
        }


    
    }
}
