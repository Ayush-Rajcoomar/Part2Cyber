using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPFModernVerticalMenu.Pages;


namespace WPFModernVerticalMenu.Pages
{
    public partial class Settings : Page
    {
        // Sample data
        private List<ServiceRequest> requests;
        private Dictionary<int, string> requestLocations; // To track locations of requests

        public Settings()
        {
            InitializeComponent();

            // Create a list of ServiceRequest objects with updated location names
            requests = new List<ServiceRequest>
            {
                new ServiceRequest { Id = 1, Description = "Leaky Faucet", Status = "Pending", Priority = "High", Location = "Umhlanga" },
                new ServiceRequest { Id = 2, Description = "Broken Window", Status = "In Progress", Priority = "Medium", Location = "Phoenix" },
                new ServiceRequest { Id = 3, Description = "Electricity Outage", Status = "Completed", Priority = "High", Location = "Durban North" },
                new ServiceRequest { Id = 4, Description = "Water Leak", Status = "Pending", Priority = "Low", Location = "Chatsworth" },
                new ServiceRequest { Id = 5, Description = "Mold Remediation", Status = "In Progress", Priority = "High", Location = "Phoenix" },
                new ServiceRequest { Id = 6, Description = "Pothole Repair", Status = "Completed", Priority = "Medium", Location = "Richards Bay" },
                new ServiceRequest { Id = 7, Description = "Fence Repair", Status = "Pending", Priority = "Low", Location = "Durban North" },
                new ServiceRequest { Id = 8, Description = "Road Maintenance", Status = "In Progress", Priority = "High", Location = "Umhlanga" },
                new ServiceRequest { Id = 9, Description = "Streetlight Outage", Status = "Completed", Priority = "Medium", Location = "Chatsworth" },
                new ServiceRequest { Id = 10, Description = "Garbage Collection", Status = "Pending", Priority = "Low", Location = "Richards Bay" }
            };

            // Set the DataGrid's ItemsSource to the list of requests
            dgRequests.ItemsSource = requests;

            // Map request IDs to locations for easy lookup when filtering related requests
            requestLocations = requests.ToDictionary(r => r.Id, r => r.Location);

            // Populate the Location ComboBox with updated location names
            var locations = new List<string> { "Umhlanga", "Phoenix", "Chatsworth", "Richards Bay", "Durban North" };
            cmbLocation.ItemsSource = locations;
        }

        // Handle search functionality
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Filter requests by ID or location (if both fields are filled)
            var searchId = txtSearchId.Text;
            var selectedLocation = cmbLocation.SelectedItem?.ToString();

            var filteredRequests = requests.Where(r =>
                (string.IsNullOrEmpty(searchId) || r.Id.ToString().Contains(searchId)) &&
                (string.IsNullOrEmpty(selectedLocation) || r.Location == selectedLocation)
            ).ToList();

            dgRequests.ItemsSource = filteredRequests;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearchId.Clear();
            cmbLocation.SelectedItem = null;
            dgRequests.ItemsSource = requests; // Reset to show all requests
        }

        // Handle selection of an item in dgRequests
        private void dgRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRequests.SelectedItem is ServiceRequest selectedRequest)
            {
                // Update the requestDetailsGrid with selected request details
                txtId.Text = selectedRequest.Id.ToString();
                txtDescription.Text = selectedRequest.Description;
                txtStatus.Text = selectedRequest.Status;
                txtLocation.Text = selectedRequest.Location;
                txtPriority.Text = selectedRequest.Priority;

                // Filter related requests by location
                var relatedRequests = requests.Where(r => r.Location == selectedRequest.Location).ToList();
                dgRelatedRequests.ItemsSource = relatedRequests;
            }
        }

        // Handle clicking the "Calculate Optimal Route" button
        private void btnCalculateRoute_Click(object sender, RoutedEventArgs e)
        {
            // Ensure a request is selected
            if (dgRequests.SelectedItem is ServiceRequest selectedRequest)
            {
                // Get the list of requests in the same location as the selected request
                var locationRequests = requests.Where(r => r.Location == selectedRequest.Location).ToList();

                // Create a MaxHeap and insert all location-specific requests
                var maxHeap = new MaxHeap();
                maxHeap.InsertAll(locationRequests);

                // Extract all requests in priority order
                var optimizedRoute = maxHeap.ExtractAll();

                // Prepare route data for DataGrid
                var routeData = optimizedRoute.Select((request, index) => new RouteData
                {
                    From = $"Request {request.Id}",
                    To = request.Description,
                    Weight = request.Priority
                }).ToList();

                // Populate the Optimal Route DataGrid
                dgOptimalRoute.ItemsSource = routeData;
            }
            else
            {
                MessageBox.Show("Please select a service request first.", "Route Calculation Error");
            }
        }

        // Handle clicking the "Back" button
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Home());
        }
    }

    // ServiceRequest class definition
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Location { get; set; } // Add Location property
    }


    public class RouteData
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Weight { get; set; }
    }

}
