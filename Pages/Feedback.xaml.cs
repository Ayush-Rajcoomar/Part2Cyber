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

namespace WPFModernVerticalMenu.Pages
{
   
    public partial class Feedback : Page
    {
        public Feedback()
        {
            InitializeComponent();
        }



        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using CyberMunicipality!", "SurveyPage", MessageBoxButton.OK);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }
    }
}
