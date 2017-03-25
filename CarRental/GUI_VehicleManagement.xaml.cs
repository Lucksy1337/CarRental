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
        #region Variables

        private DM_DBConnection databaseConnection;
        private CL_List list;
        #endregion

        #region Constructor
        public GUI_VehicleManagement()
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
            LoadComboBoxVehicleType();
            LoadComboBoxInsurancePackage();
        }

        private void LoadComboBoxVehicleType()
        {
            comboBoxVehicleType.Items.Clear();
            foreach (Fahrzeugtyp vehicleType in list.VehicleTypeList)
            {
                comboBoxVehicleType.Items.Add(vehicleType);
            }
        }

        private void LoadComboBoxInsurancePackage()
        {
            comboBoxInsurancePackage.Items.Clear();
            foreach (Versicherungspaket insurancePackage in list.InsurancePackageList)
            {
                comboBoxInsurancePackage.Items.Add(insurancePackage);
            }
        }

        private void LoadListBoxCreatedVehicles()
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

        private void CreateVehicle()
        {
            Fahrzeug newVehicle = null;
            bool textHasSpace = false;
            bool textWrongInput = false;

            if (!textBoxDescription.Text.Contains(" ") && !textBoxBrand.Text.Contains(" ") && !textBoxVintage.Text.Contains(" ") && !textBoxMileage.Text.Contains(" ") &&
                !textBoxGearChange.Text.Contains(" ") && !textBoxSeats.Text.Contains(" ") && !textBoxDoors.Text.Contains(" ") && !textBoxRentPerDay.Text.Contains(" "))
            {
                if (!textBoxDescription.Text.Equals("") && !textBoxBrand.Text.Equals("") && !textBoxVintage.Text.Equals("") && !textBoxMileage.Text.Equals("") && comboBoxVehicleType.SelectedItem != null &&
                comboBoxInsurancePackage.SelectedItem != null && !textBoxGearChange.Text.Equals("") && !textBoxSeats.Text.Equals("") && !textBoxDoors.Text.Equals("") && !textBoxRentPerDay.Text.Equals("") && !HasTextInputOnlyCommas(textBoxRentPerDay.Text))
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
                var result = MessageBox.Show("Möchten Sie ein neues Fahrzeug anlegen?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ClearComponents();
                    list.addToVehicleList(newVehicle);
                    LoadListBoxCreatedVehicles();
                    comboBoxVehicleType.SelectedItem = newVehicle.Fahrzeugtyp;
                }
            }
        }

        private void ModifyVehicle()
        {
            Fahrzeug aVehicle = (Fahrzeug)listBoxCreatedVehicles.SelectedItem;
            bool textHasSpace = false;
            bool textWrongInput = false;

            if (!textBoxDescription.Text.Contains(" ") && !textBoxBrand.Text.Contains(" ") && !textBoxVintage.Text.Contains(" ") && !textBoxMileage.Text.Contains(" ") &&
                !textBoxGearChange.Text.Contains(" ") && !textBoxSeats.Text.Contains(" ") && !textBoxDoors.Text.Contains(" ") && !textBoxRentPerDay.Text.Contains(" "))
            {
                if (!textBoxDescription.Text.Equals("") && !textBoxBrand.Text.Equals("") && !textBoxVintage.Text.Equals("") && !textBoxMileage.Text.Equals("") &&
                !textBoxGearChange.Text.Equals("") && !textBoxSeats.Text.Equals("") && !textBoxDoors.Text.Equals("") && !textBoxRentPerDay.Text.Equals("") && !HasTextInputOnlyCommas(textBoxRentPerDay.Text))
                {
                    aVehicle.Bezeichnung = textBoxDescription.Text;
                    aVehicle.Marke = textBoxBrand.Text;
                    aVehicle.Baujahr = Convert.ToInt32(textBoxVintage.Text);
                    aVehicle.Kilometerstand = Convert.ToInt32(textBoxMileage.Text);
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
                var result = MessageBox.Show("Möchten Sie die Fahrzeugdaten speichern?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    list.VehicleList.Remove(aVehicle);
                    list.addToVehicleList(aVehicle);
                    LoadListBoxCreatedVehicles();

                    MessageBox.Show("Fahrzeugdaten wurden erfolgreich gespeichert.");
                }
            }
        }

        private void DeleteVehicle()
        {
            Fahrzeug deletingVehicle = (Fahrzeug)listBoxCreatedVehicles.SelectedItem;

            if (deletingVehicle.Verfügbar)
            {
                var result = MessageBox.Show("Möchten Sie das ausgewählten Fahrzeug entfernen?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (list.VehicleList != null && list.VehicleSortedByTypeList != null)
                    {
                        list.VehicleList.Remove(deletingVehicle);
                        LoadListBoxCreatedVehicles();

                        MessageBox.Show("Das ausgewählte Fahrzeug wurde entfernt");
                    }
                }
            }
            else
            {
                MessageBox.Show("Das ausgewählte Fahrzeug kann nicht gelöscht werden, da es verliehen ist.");
            }
        }        

        public void UpdateTextBoxes(Fahrzeug selectedVehicle)
        {
            bool cancelVehicleCreation = false;

            if (buttonCreateVehicle.IsEnabled && listBoxCreatedVehicles.SelectedItem != null)
            {
                var result = MessageBox.Show("Möchten Sie die Fahrzeugerstellung verlassen?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

                    EnableVehicleComponents(true);
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

        private void PrepareComponentsForNewVehicleCreation()
        {
            EnableVehicleComponents(true);
            buttonDeleteVehicle.IsEnabled = false;
            buttonModifyVehicle.IsEnabled = false;
            buttonCreateVehicle.IsEnabled = true;
            ClearComponents();
            checkBoxAvailability.IsChecked = true;
        }

        private void EnableVehicleComponents(bool status)
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
        }

        private void ClearComponents()
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
        }

        private bool IsTextInputTypeOfInteger(string text)
        {
            int number;

            return int.TryParse(text, out number);
        }

        private bool HasTextInputOnlyCommas(string text)
        {
            bool status = false;

            foreach (char subString in text.ToArray())
            {
                if (subString == ',')
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
        #endregion

        #region Events

        private void ButtonNewVehicle_Click(object sender, RoutedEventArgs e)
        {
            PrepareComponentsForNewVehicleCreation();
        }

        private void ButtonModifyVehicle_Click(object sender, RoutedEventArgs e)
        {
            ModifyVehicle();
        }

        private void ButtonCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            CreateVehicle();
        }

        private void ButtonDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            DeleteVehicle();
        }

        private void comboBoxVehicleType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadListBoxCreatedVehicles();
        }

        private void listBoxCreatedVehicles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTextBoxes((Fahrzeug)listBoxCreatedVehicles.SelectedItem);
        }

        private void TextBoxVintage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxMileage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxSeats_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxDoors_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBoxRentPerDay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "," && !IsTextInputTypeOfInteger(e.Text))
            {
                e.Handled = true;
            }
        }
        #endregion
    }
}