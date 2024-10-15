using Microsoft.Win32;
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
using WPFModernVerticalMenu.Models;

namespace WPFModernVerticalMenu.Pages
{
    
    public partial class ReportIssues : Page
    {
     public static List<Issue> issueList = new List<Issue>();
        private string selectedImg;
        public ReportIssues()
        {

            InitializeComponent();
        }

        // Event handler for the "Attach Media" button
        private void AttachMedia_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select Media",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                MessageBox.Show($"Attached: {openFileDialog.FileName}", "Media Attached", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateProgress(50); // Increment progress by 25% when media is attached
                selectedImg = openFileDialog.FileName;    // url in variable
            }

        }

        // Event handler for the "Submit" button
        private string ConvertRichTextBoxContentsToString(RichTextBox rtb)    // converts richedit to string
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.ToString();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UpdateProgress(100); // Set progress to 100% on submission
            MessageBox.Show("Report Submitted Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            string location= LocationInput.Text;
            string category= CategorySelection.Text;
            string description=ConvertRichTextBoxContentsToString(DescriptionBox);
            string imgPath=selectedImg;


            issueList.Add(new Issue(location, category, description, imgPath));


        }

        // Event handler for the "Back to Home Page" button
        private void NavigateBack_Click(object sender, RoutedEventArgs e)
        {
           

            MessageBox.Show("Navigating to Home Page...", "Navigation", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateProgress(0);

            this.NavigationService.Navigate(new Home());   // navigates back to home page
            

            // Reset progress if navigating back
        }

        // Method to update the progress bar value
        private void UpdateProgress(double value)
        {
            ReportProgressBar.Value = value;
        }
    }
}

 