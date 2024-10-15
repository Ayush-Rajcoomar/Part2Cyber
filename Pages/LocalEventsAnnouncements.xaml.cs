using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic;
using WPFModernVerticalMenu.Models;

namespace WPFModernVerticalMenu.Pages
{
    public partial class LocalEventsAnnouncements : Page
    {
        private EventDataDictionary eventData;
        private DateTime selectedDate;
        private Stack<List<string>> EventStack = new Stack<List<string>>();

        // Dictionary to store how many times a date has been searched
        private Dictionary<DateTime, int> dateSearchCount = new Dictionary<DateTime, int>();

        public LocalEventsAnnouncements()
        {
            InitializeComponent();
            eventData = new EventDataDictionary(); // Initialize the event data dictionary
            LoadStack(); // Call LoadStack to display recent events
        }

        // Event handler for calendar selection change
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = ((Calendar)sender).SelectedDate ?? DateTime.Now;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (selectedDate != null)
            {
                var events = eventData.GetEventsForDate(selectedDate);

                // Increment search count for the selected date
                UpdateSearchCount(selectedDate);

                if (events.Any())
                {
                    MessageBox.Show(string.Join("\n", events), $"Events on {selectedDate.ToShortDateString()}");
                }
                else
                {
                    MessageBox.Show("No events found for this date.", "No Events");
                }
            }
            else
            {
                MessageBox.Show("Please select a date from the calendar.", "No Date Selected");
            }
        }

        // Event handler for adding events via the InputBox
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string inputDate = Interaction.InputBox("Enter the event date (format: YYYY-MM-DD):", "Add Event", DateTime.Now.ToString("yyyy-MM-dd"));

            if (DateTime.TryParse(inputDate, out DateTime selectedDateInput))
            {
                string eventTitle = Interaction.InputBox("Enter the event title:", "Add Event", "");

                if (!string.IsNullOrEmpty(eventTitle))
                {
                    var eventsForDate = eventData.GetEventsForDate(selectedDateInput);

                    if (eventsForDate.Contains(eventTitle))
                    {
                        MessageBox.Show($"The event '{eventTitle}' already exists for {selectedDateInput.ToShortDateString()}.", "Duplicate Event");
                    }
                    else
                    {
                        eventData.AddEventForDate(selectedDateInput, eventTitle);
                        MessageBox.Show($"Event '{eventTitle}' added for {selectedDateInput.ToShortDateString()}");
                    }
                }
                else
                {
                    MessageBox.Show("Event title cannot be empty.", "Invalid Input");
                }
            }
            else
            {
                MessageBox.Show("Invalid date format. Please use the format: YYYY-MM-DD.", "Invalid Input");
            }
        }

        // Initialize the EventStack
        private void InitialisStack()
        {
            foreach (var eventList in eventData.GetAllEvents())
            {
                EventStack.Push(eventList);
            }
        }

        // Load recent events into the UI
        private void LoadStack()
        {
            InitialisStack();

            for (int i = 0; i < 3 && EventStack.Any(); i++)
            {
                var localEvents = EventStack.Pop();
                foreach (var eventDetail in localEvents)
                {
                    TextBlock textBlock = new TextBlock()
                    {
                        Text = $"Event: {eventDetail}",
                        Foreground = Brushes.Black,
                        FontSize = 14,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Border border = new Border
                    {
                        Child = textBlock,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(2),
                        Background = Brushes.LightCyan,
                        Padding = new Thickness(5),
                        Margin = new Thickness(10)
                    };

                    stackMostRecent.Children.Add(border); // Add the event details to the StackPanel
                }
            }
        }

        // Method to update the search count for a selected date
        private void UpdateSearchCount(DateTime selectedDate)
        {
            // Check if the date is already in the dictionary
            if (dateSearchCount.ContainsKey(selectedDate))
            {
                // Increment the search count for the date
                dateSearchCount[selectedDate]++;
            }
            else
            {
                // Add the date with an initial count of 1
                dateSearchCount[selectedDate] = 1;
            }

            // If the search count is more than 2, add it to the recommendations
            if (dateSearchCount[selectedDate] > 2)
            {
                AddDateToRecommendations(selectedDate);
            }
        }


        // Method to add the date to the recommendations stack
        private void AddDateToRecommendations(DateTime date)
        {
            // Create a label for the recommended date
            TextBlock recommendationTextBlock = new TextBlock
            {
                Text = $"Recommended Search:\n {date.ToShortDateString()}",
                Foreground = Brushes.Black,
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create a border around the recommendation
            Border recommendationBorder = new Border
            {
                Child = recommendationTextBlock,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Background = Brushes.LightCyan,
                Padding = new Thickness(5),
                Margin = new Thickness(10)
            };

            // Add the border to the recommendations stack panel
            stackReccomend.Children.Add(recommendationBorder);
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
            {
                if (selectedDate != null)
                {
                    var events = eventData.GetEventsForDate(selectedDate);

                    if (events.Any())
                    {
                        // Show the events to the user
                        string eventsList = string.Join("\n", events);
                        string deletePrompt = $"Select an event to delete from the following:\n{eventsList}\n\nType the exact event title to delete it (or type 'all' to delete all events):";
                        string userInput = Interaction.InputBox(deletePrompt, "Delete Event", "");

                        if (!string.IsNullOrEmpty(userInput))
                        {
                            if (userInput.ToLower() == "all")
                            {
                                // Delete all events for the selected date
                                eventData.DeleteEventsForDate(selectedDate);
                                MessageBox.Show($"All events deleted for {selectedDate.ToShortDateString()}.", "Events Deleted");
                            }
                            else if (events.Contains(userInput))
                            {
                                // Delete the specific event
                                eventData.DeleteEventForDate(selectedDate, userInput);
                                MessageBox.Show($"Event '{userInput}' deleted for {selectedDate.ToShortDateString()}.", "Event Deleted");
                            }
                            else
                            {
                                MessageBox.Show("Event not found. Please check the title and try again.", "Invalid Event Title");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No input provided. Deletion cancelled.", "No Input");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No events found for this date.", "No Events");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a date from the calendar.", "No Date Selected");
                }
            }

        
    }

    // EventDataDictionary Class
    public class EventDataDictionary
    {
        private Dictionary<DateTime, List<string>> events;

        public EventDataDictionary()
        {
            events = new Dictionary<DateTime, List<string>>();
            LoadEvents(); // Load hardcoded events
        }

        private void LoadEvents() // hardcoded data 
        {
            events[new DateTime(2024, 10, 1)] = new List<string> { "Sustainability Workshop", "Local Farmers Meetup" };
            events[new DateTime(2024, 10, 2)] = new List<string> { "Renewable Energy Expo", "Agri-Tech Conference" };
            events[new DateTime(2024, 10, 3)] = new List<string> { "Community Garden Planting", "Drone Technology Showcase" };
            events[new DateTime(2024, 10, 4)] = new List<string> { "Urban Farming Seminar", "Water Conservation Talk" };
            events[new DateTime(2024, 10, 5)] = new List<string> { "Sustainable Business Networking", "Organic Market Fair" };
            events[new DateTime(2024, 10, 6)] = new List<string> { "Soil Health Workshop", "AI in Agriculture Webinar" };
            events[new DateTime(2024, 10, 7)] = new List<string> { "Renewable Energy for Farms", "Local Art Exhibition" };
            events[new DateTime(2024, 10, 8)] = new List<string> { "Beekeeping Essentials", "Agri-FinTech Innovations" };
            events[new DateTime(2024, 10, 9)] = new List<string> { "Community Clean-Up Drive", "Food Preservation Workshop" };
            events[new DateTime(2024, 10, 10)] = new List<string> { "Permaculture Principles", "Hydroponics Training" };
            events[new DateTime(2024, 10, 11)] = new List<string> { "Climate Change Discussion", "Tech in Agri-Business Seminar" };
            events[new DateTime(2024, 10, 12)] = new List<string> { "Green Energy Innovations", "Biodiversity Conservation Workshop" };
            events[new DateTime(2024, 10, 13)] = new List<string> { "Farmers' Cooperative Meeting", "Energy-Efficient Farming Practices" };
            events[new DateTime(2024, 10, 14)] = new List<string> { "Aquaponics Training", "Sustainable Living Fair" };
            events[new DateTime(2024, 10, 15)] = new List<string> { "Soil Regeneration Techniques", "Smart Irrigation Systems" };
            events[new DateTime(2024, 10, 16)] = new List<string> { "Urban Agriculture Strategies", "Farm-to-Table Movement Talk" };
            events[new DateTime(2024, 10, 17)] = new List<string> { "Food Waste Reduction Workshop", "Solar Energy in Agriculture" };
            events[new DateTime(2024, 10, 18)] = new List<string> { "Composting Workshop", "Biogas Technology Overview" };
            events[new DateTime(2024, 10, 19)] = new List<string> { "Greenhouse Management", "Sustainable Packaging Seminar" };
            events[new DateTime(2024, 10, 20)] = new List<string> { "Vertical Farming Insights", "Agroforestry Workshop" };
            events[new DateTime(2024, 10, 21)] = new List<string> { "Eco-Friendly Pest Control", "Electric Vehicles for Agriculture" };
            events[new DateTime(2024, 10, 22)] = new List<string> { "Renewable Energy Solutions", "Local Food Producers Market" };
            events[new DateTime(2024, 10, 23)] = new List<string> { "Water Recycling Techniques", "Farmers' Innovation Lab" };
            events[new DateTime(2024, 10, 24)] = new List<string> { "Agricultural Policy Discussion", "Women in Agriculture Panel" };
            events[new DateTime(2024, 10, 25)] = new List<string> { "AI for Crop Monitoring", "Community Seed Exchange" };
            events[new DateTime(2024, 10, 26)] = new List<string> { "Sustainable Fisheries Talk", "Plant-Based Nutrition Seminar" };
            events[new DateTime(2024, 10, 27)] = new List<string> { "Smart Farming Solutions", "Water Harvesting Techniques" };
            events[new DateTime(2024, 10, 28)] = new List<string> { "Electric Tractors Demo", "Farmers' Mental Health Discussion" };
            events[new DateTime(2024, 10, 29)] = new List<string> { "Regenerative Agriculture", "Food Security Strategies" };
            events[new DateTime(2024, 10, 30)] = new List<string> { "Workshop on AI", "Team Building Event" };
        }


        // Get events for a specific date
        public List<string> GetEventsForDate(DateTime date)
        {
            return events.ContainsKey(date) ? events[date] : new List<string>();
        }

        // Get all events (for stack loading)
        public IEnumerable<List<string>> GetAllEvents()
        {
            return events.Values;
        }

        // Add an event for a specific date
        public void AddEventForDate(DateTime date, string eventTitle)
        {
            if (!events.ContainsKey(date))
            {
                events[date] = new List<string>();
            }
            events[date].Add(eventTitle);
        }
        // Delete a specific event for a given date
        public void DeleteEventForDate(DateTime date, string eventTitle)
        {
            if (events.ContainsKey(date))
            {
                events[date].Remove(eventTitle);

                // Remove the date entry if no events are left
                if (!events[date].Any())
                {
                    events.Remove(date);
                }
            }
        }

        // Delete all events for a given date
        public void DeleteEventsForDate(DateTime date)
        {
            if (events.ContainsKey(date))
            {
                events.Remove(date);
            }
        }


    }
}
