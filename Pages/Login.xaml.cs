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

    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = LoginEmailTextBox.Text;
                string password = LoginPasswordBox.Password;

                var response = await FirebaseAuthService.LoginUserAsync(email, password);
                MessageBox.Show("Login successful!");
                this.NavigationService.Navigate(new Dashboard());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed: {ex.Message}");
            }


        }
    }





}