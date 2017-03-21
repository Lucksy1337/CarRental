//using CarRental.CarRentalServiceReference;
using CarRental.CarRentalSchoolServiceReference;
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
            foreach (Fahrzeugtyp vehicleType in list.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(vehicleType);
            }
        }

        private void loadComboBoxInsurancePackage()
        {
            comboBoxInsurancePackage.Items.Clear();
            foreach (Versicherungspaket insurancePackage in list.InsurancePackageList)
            {
                comboBoxInsurancePackage.Items.Add(insurancePackage);
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

                if (!buttonCreateVehicle.IsEnabled)
                {
                    listBoxCreatedVehicles.SelectedIndex = 0;
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
            bool textHasSpace = false;
            bool textWrongInput = false;

            if (!textBoxDescription.Text.Contains(" ") && !textBoxBrand.Text.Contains(" ") && !textBoxVintage.Text.Contains(" ") && !textBoxMileage.Text.Contains(" ") &&
                !textBoxGearChange.Text.Contains(" ") && !textBoxSeats.Text.Contains(" ") && !textBoxDoors.Text.Contains(" ") && !textBoxRentPerDay.Text.Contains(" "))
            {
                if (!textBoxDescription.Text.Equals("") && !textBoxBrand.Text.Equals("") && !textBoxVintage.Text.Equals("") && !textBoxMileage.Text.Equals("") &&
                !textBoxGearChange.Text.Equals("") && !textBoxSeats.Text.Equals("") && !textBoxDoors.Text.Equals("") && !textBoxRentPerDay.Text.Equals("") && !hasTextInputOnlyCommas(textBoxRentPerDay.Text))
                {
                    aVehicle.Bezeichnung = textBoxDescription.Text;
                    aVehicle.Marke = textBoxBrand.Text;
                    aVehicle.Baujahr = Convert.ToInt32(textBoxVintage.Text);
                    aVehicle.Kilometerstand = Convert.ToDouble(textBoxMileage.Text);
                    aVehicle.Schaltung = textBoxGearChange.Text;
                    aVehicle.Sitze = Convert.ToInt32(textBoxSeats.Text);
                    aVehicle.Türe = Convert.ToInt32(textBoxDoors.Text);
                    aVehicle.Naviagationssystem = Convert.ToBoolean(checkBoxNavigation.IsChecked);
                    aVehicle.Klimaanlage = Convert.ToBoolean(checkBoxAirConditioning.IsChecked);
                    aVehicle.MietpreisProTag = Convert.ToDouble(textBoxRentPerDay.Text);
                    aVehicle.Verfügbar = Convert.ToBoolean(checkBoxAvailability.IsChecked);
                }
                else
                {
                    textWrongInput = true;
                    MessageBox.Show("Bitte füllen Sie die Fahrzeugdaten korrekt aus.");
                }
            }
            else
            {
                textHasSpace = true;
                MessageBox.Show("In Ihren Fahrzeugdaten dürfen keine Leerzeichen enthalten sein.");
            }

            if (!textWrongInput && !textHasSpace)
            {
                var result = MessageBox.Show("Möchten Sie die Fahrzeugdaten speichern?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    list.VehicleList.Remove(aVehicle);
                    list.addToVehicleList(aVehicle);
                    loadListBoxCreatedVehicles();

                    MessageBox.Show("Fahrzeugdaten wurden erfolgreich gespeichert.");
                }
            }
        }

        private void buttonCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            Fahrzeug newVehicle = null;
            bool textHasSpace = false;
            bool textWrongInput = false;

            if (!textBoxDescription.Text.Contains(" ") && !textBoxBrand.Text.Contains(" ") && !textBoxVintage.Text.Contains(" ") && !textBoxMileage.Text.Contains(" ") &&
                !textBoxGearChange.Text.Contains(" ") && !textBoxSeats.Text.Contains(" ") && !textBoxDoors.Text.Contains(" ") && !textBoxRentPerDay.Text.Contains(" "))
            {
                if (!textBoxDescription.Text.Equals("") && !textBoxBrand.Text.Equals("") && !textBoxVintage.Text.Equals("") && !textBoxMileage.Text.Equals("") && comboBoxVehicleType.SelectedItem != null &&
                comboBoxInsurancePackage.SelectedItem != null && !textBoxGearChange.Text.Equals("") && !textBoxSeats.Text.Equals("") && !textBoxDoors.Text.Equals("") && !textBoxRentPerDay.Text.Equals("") && !hasTextInputOnlyCommas(textBoxRentPerDay.Text))
                {
                    Fahrzeugtyp aVehicleType = (Fahrzeugtyp)comboBoxVehicleType.SelectedItem;
                    Versicherungspaket aInsurancePackage = (Versicherungspaket)comboBoxInsurancePackage.SelectedItem;

                    newVehicle = new Fahrzeug();
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
                    newVehicle.Türe = Convert.ToInt32(textBoxDoors.Text);
                    newVehicle.Naviagationssystem = Convert.ToBoolean(checkBoxNavigation.IsChecked);
                    newVehicle.Klimaanlage = Convert.ToBoolean(checkBoxAirConditioning.IsChecked);
                    newVehicle.MietpreisProTag = Convert.ToDouble(textBoxRentPerDay.Text);
                    newVehicle.Verfügbar = Convert.ToBoolean(checkBoxAvailability.IsChecked);
                }
                else
                {
                    textWrongInput = true;
                    MessageBox.Show("Bitte füllen Sie die Fahrzeugdaten korrekt aus.");
                }
            }
            else
            {
                textHasSpace = true;
                MessageBox.Show("In Ihren Fahrzeugdaten dürfen keine Leerzeichen enthalten sein.");
            }

            if (!textWrongInput && !textHasSpace)
            {
                var result = MessageBox.Show("Möchten Sie ein neues Fahrzeug anlegen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    clearComponents();
                    list.addToVehicleList(newVehicle);
                    loadListBoxCreatedVehicles();
                    comboBoxVehicleType.SelectedItem = newVehicle.Fahrzeugtyp;
                }
            }
        }

        private void buttonDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            Fahrzeug deletingVehicle = (Fahrzeug)listBoxCreatedVehicles.SelectedItem;

            if (!deletingVehicle.Verfügbar)
            {
                var result = MessageBox.Show("Möchten Sie das ausgewählten Fahrzeug entfernen?", "caption", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (list.VehicleList != null && list.VehicleSortedByTypeList != null)
                    {

                        list.VehicleList.Remove(deletingVehicle);
                        loadListBoxCreatedVehicles();

                        MessageBox.Show("Das ausgewählte Fahrzeug wurde entfernt");
                    }
                }
            }
            else
            {
                MessageBox.Show("Das ausgewählte Fahrzeug kann nicht gelöscht werden, da es verliehen ist.");
            }
        }

        private void textBoxVintage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void textBoxMileage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void textBoxSeats_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void textBoxDoors_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void textBoxRentPerDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "," && !isTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
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
                    textBoxDoors.Text = Convert.ToString(selectedVehicle.Türe);
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
            textBoxDoors.IsEnabled = status;
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
            textBoxDoors.Text = null;
            checkBoxNavigation.IsChecked = false;
            checkBoxAirConditioning.IsChecked = false;
            textBoxRentPerDay.Text = null;
            checkBoxAvailability.IsChecked = false;
        }

        private bool isTextInputTypeOfInteger(string text)
        {
            int number;

            return int.TryParse(text, out number);
        }

        private bool hasTextInputOnlyCommas(string text)
        {
            bool status = false;

            foreach (char stringSub in text.ToArray())
            {
                if (stringSub == ',')
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }

            return status;
        }
    }
}