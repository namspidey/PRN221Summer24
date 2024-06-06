using LoadDB.Models;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoadDB
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            var st = PRN211_1Context.INSTANCE.Students.Include(x => x.Depart).ToList();
            dgvDisplay.ItemsSource = st;
            var department = PRN211_1Context.INSTANCE.Departments.ToList();
            cbxDepart.ItemsSource = department;
            cbxDepartName.ItemsSource = department;
        }

        private void cbxDepart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            // Clear ComboBox selection
            cbxDepart.SelectedIndex = -1;

            // Uncheck radio buttons
            rdbFe.IsChecked = false;
            rdbMa.IsChecked = false;

            // Clear TextBox
            txtNameContain.Text = "";
            Load();
        }

        private void rdbFe_Checked(object sender, RoutedEventArgs e)
        {
            FilterData();
        }

        private void rdbMa_Checked(object sender, RoutedEventArgs e)
        {
            FilterData();
        }
        private void txtNameContain_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }
        private void FilterData()
        {
            var selectDepart = cbxDepart.SelectedValue;
            var filterDepart = PRN211_1Context.INSTANCE.Students
                                        .Include(x => x.Depart).ToList();
            if (selectDepart != null)
            {
                filterDepart = filterDepart.Where(x => x.Depart.Id == selectDepart).ToList();
            }
            bool? selectedSex = null;
            if (rdbFe.IsChecked == true)
                selectedSex = false;
            else if (rdbMa.IsChecked == true)
                selectedSex = true;
            if (selectedSex != null)
            {
                filterDepart = filterDepart.Where(x => x.Gender == selectedSex).ToList();
            }
            if (!string.IsNullOrEmpty(txtNameContain.Text))
            {
                string searchText = txtNameContain.Text.ToLower(); 
                filterDepart = filterDepart.Where(x => x.Name.ToLower().Contains(searchText)).ToList();
            }

            dgvDisplay.ItemsSource = filterDepart;
        }

        private void bntInsert_Click(object sender, RoutedEventArgs e)
        {
            Student st = GetStudent();
            var x = PRN211_1Context.INSTANCE.Students.Find(st.Id);
            if (x != null){
                return;
            }   
            PRN211_1Context.INSTANCE.Students.Add(st);
            PRN211_1Context.INSTANCE.SaveChanges();
            Load();

        }

        private void bntUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student st = GetStudent();
            var x = PRN211_1Context.INSTANCE.Students.Find(st.Id);
            if (x == null)
            {
                return;
            }
            x.Name = st.Name;
            x.Gender = st.Gender;
            x.DepartId = st.DepartId;   
            x.Dob = st.Dob;
            x.Gpa = st.Gpa;
            PRN211_1Context.INSTANCE.Update(x);
            PRN211_1Context.INSTANCE.SaveChanges();
            Load();

        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            var x = PRN211_1Context.INSTANCE.Students.Find(int.Parse(txtID.Text));
            if (x==null)
            {
                MessageBox.Show("No Id like that");
                return;   
            }
            PRN211_1Context.INSTANCE.Remove(x);
            PRN211_1Context.INSTANCE.SaveChanges();
            Load();
        }
        private Student GetStudent()
        {
            int id = int.Parse(txtID.Text);
            string name = txtName.Text;
            bool gender = chkGender.IsChecked == true;
            string departId = PRN211_1Context.INSTANCE.Departments.FirstOrDefault(x => x.Id == cbxDepartName.SelectedValue).Id;
            DateTime dob = dpkDOB.SelectedDate.Value;
            float gpa = float.Parse(txtGPA.Text);
            return new Student
            {
                Id = id,
                Name = name,
                Gender = gender,
                Dob = dob,
                Gpa = gpa,
                DepartId=departId
            };
        }
    }
}
