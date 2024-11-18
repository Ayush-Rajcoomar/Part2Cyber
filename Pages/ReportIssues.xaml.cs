using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using WPFModernVerticalMenu.Models;
using WPFModernVerticalMenu.Services; // Added for TreeManager

namespace WPFModernVerticalMenu.Pages
{
    public partial class ReportIssues : Page
    {
        public static List<Issue> issueList = new List<Issue>();
        private string selectedImg;

        // TreeManager instance to handle tree operations
        private static TreeManager treeManager = new TreeManager();

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
                UpdateProgress(50);
                selectedImg = openFileDialog.FileName;
            }
        }

        // Converts RichTextBox content to string
        private string ConvertRichTextBoxContentsToString(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text.Trim(); // Trim to remove extra line breaks
        }

        // Event handler for the "Submit" button
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UpdateProgress(100);
            MessageBox.Show("Report Submitted Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Get input data from UI elements
            string location = LocationInput.Text;
            string category = CategorySelection.Text;
            string description = ConvertRichTextBoxContentsToString(DescriptionBox);
            string imgPath = selectedImg;

            // Add the issue to the issueList
            Issue newIssue = new Issue(location, category, description, imgPath);
            issueList.Add(newIssue);

            // Also add the issue to the tree structure using TreeManager
            ReportedIssue reportedIssue = new ReportedIssue(location, category, description, imgPath);
            treeManager.AddIssue(reportedIssue);
        }

        // Event handler for the "Back to Home Page" button
        private void NavigateBack_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigating to Home Page...", "Navigation", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateProgress(0);
            this.NavigationService.Navigate(new Home());
        }

        // Method to update the progress bar value
        private void UpdateProgress(double value)
        {
            ReportProgressBar.Value = value;
        }

        // Method to get the TreeManager instance (for use in other pages)
        public static TreeManager GetTreeManager()
        {
            return treeManager;
        }
    }
}
