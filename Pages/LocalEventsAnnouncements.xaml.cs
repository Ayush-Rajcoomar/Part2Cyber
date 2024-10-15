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
                    eventData.AddEventForDate(selectedDateInput, eventTitle);
                    MessageBox.Show($"Event '{eventTitle}' added for {selectedDateInput.ToShortDateString()}");
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

        private void LoadEvents()
        {
            // Hardcoded Events Data
            events[new DateTime(2024, 10, 1)] = new List<string> { "Sustainability Workshop", "Local Farmers Meetup" };
            events[new DateTime(2024, 10, 2)] = new List<string> { "Renewable Energy Expo", "Agri-Tech Conference" };
            // ... (other events)
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
    }
}
