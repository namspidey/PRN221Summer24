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

namespace DemoRadioButton
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
        /*
         * private void Button_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;
            String an1 = RadioButton_Checked1(sender, e);
            String an2 = RadioButton_Checked2(sender, e);
            String an3 = RadioButton_Checked3(sender, e);
            if (an1.Equals("2"))
            {
                score += 1;
            }
            if (an1.Equals("24"))
            {
                score += 1;
            }
            if (an1.Equals("5"))
            {
                score += 1;
            }
            if ( score == 0)
            {
                MessageBox.Show($"Stupid{score}/3");
            }
            else if(score == 1 || score == 2)
            {
                MessageBox.Show($"OK{score}/3");
            }
            else
            {
                MessageBox.Show($"Good{score}/3");
            }

        }

        private string RadioButton_Checked1(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton1 = (RadioButton)sender;
            if(radioButton1 != null) { return radioButton1.Content.ToString(); }
            else { return "No answer"; }
        }

        private string RadioButton_Checked2(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton2 = (RadioButton)sender;
            if (radioButton2 != null) { return radioButton2.Content.ToString(); }
            else { return "No answer"; }
        }

        private string RadioButton_Checked3(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton3 = (RadioButton)sender;
            if (radioButton3 != null) { return radioButton3.Content.ToString(); }
            else { return "No answer"; }
        }
         */

    }
}
