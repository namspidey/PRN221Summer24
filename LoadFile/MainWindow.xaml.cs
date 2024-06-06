using LoadFile.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace LoadFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Person> per = new List<Person>();
        List<Person> perFilter = new List<Person>();
        private void dgDisplay_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedCells.Count > 0)
            {
                DataGridCellInfo cellInfo = dataGrid.SelectedCells[0];
                if (cellInfo.IsValid)
                {
                    DataGridCell cell = cellInfo.Column.GetCellContent(cellInfo.Item).Parent as DataGridCell;
                    if (cell != null)
                    {
                        TextBlock cellContent = cell.Content as TextBlock;
                        if (cellContent != null)
                        {
                            string cellValue = cellContent.Text;
                            txtID.Text = cellValue; // Assuming txtID is a TextBox control
                        }
                    }
                }

                DataGridCellInfo cellNameInfo = dataGrid.SelectedCells[1];
                if (cellNameInfo.IsValid)
                {
                    DataGridCell cell = cellNameInfo.Column.GetCellContent(cellNameInfo.Item).Parent as DataGridCell;
                    if (cell != null)
                    {
                        TextBlock cellContent = cell.Content as TextBlock;
                        if (cellContent != null)
                        {
                            string cellValue = cellContent.Text;
                            txtName.Text = cellValue; // Assuming txtID is a TextBox control
                        }
                    }
                }

                DataGridCellInfo cellGenderInfo = dataGrid.SelectedCells[2];
                if (cellGenderInfo.IsValid)
                {
                    DataGridCell cell = cellGenderInfo.Column.GetCellContent(cellGenderInfo.Item).Parent as DataGridCell;
                    if (cell != null)
                    {
                        TextBlock cellContent = cell.Content as TextBlock;
                        if (cellContent != null)
                        {
                            string cellValue = cellContent.Text;
                            if (Boolean.Parse(cellValue))
                            {
                                ckbGender.IsChecked = true;

                            }
                            else
                            {
                                ckbGender.IsChecked = false;
                            }
                        }
                    }
                }
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
            };

            bool? result = ofd.ShowDialog();
            if (result != true) return;

            try
            {
                var xs = new XmlSerializer(typeof(List<Person>));
                using Stream s2 = File.OpenRead(ofd.FileName);
                per = (List<Person>)xs.Deserialize(s2);
                dgDisplay.ItemsSource = per;
                dgDisplay.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var sdb = new SaveFileDialog() { Filter = "XML Files (*.xml)|*.xml|All File(*.*)|*.*" };
            var result = sdb.ShowDialog();
            if (result == false) return;
            var xs = new XmlSerializer(typeof(List<Person>));
            using Stream s1 = File.Create(sdb.FileName);
            xs.Serialize(s1, per);
            s1.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var x = per.FirstOrDefault(x => x.Id == id);
            if (x != null) return
                    ;
            string name = txtName.Text;
            bool gender = (bool)ckbGender.IsChecked;
            per.Add(new Person() { Id = id, Name = name, Gender = gender });
            dgDisplay.ItemsSource = per;
            dgDisplay.Items.Refresh();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtID.Text);
            var x = per.FirstOrDefault(x=>x.Id == id);
            if (x != null) return;
            string name = txtName.Text;
            bool gender = (bool)ckbGender.IsChecked;
            per.Add(new Person { Id = id, Name = name, Gender = gender });  
            dgDisplay.ItemsSource = per;
            dgDisplay.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtID.Text);
            for (int i = 0; i < per.Count; i++)
            {
                if (per[i].Id == id)
                {
                    per.RemoveAt(i);
                    i--;
                }
            }
            dgDisplay.ItemsSource = per;
            dgDisplay.Items.Refresh();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            bool? selectedGender = null;
            if (rdbFe.IsChecked == true)
            {
                selectedGender = false; // Assuming false for Female
            }
            else if (rdbMa.IsChecked == true)
            {
                selectedGender = true; // Assuming true for Male
            }

            string nameContains = txtNameContain.Text.ToLower();

            perFilter.Clear(); // Clear the filtered list

            foreach (var person in per)
            {
                bool matchesGender = !selectedGender.HasValue || person.Gender == selectedGender.Value;
                bool matchesName = string.IsNullOrEmpty(nameContains) || person.Name.ToLower().Contains(nameContains);

                if (matchesGender && matchesName)
                {
                    perFilter.Add(person);
                }
            }

            dgDisplay.ItemsSource = perFilter;
            dgDisplay.Items.Refresh();
        }
    }

}
