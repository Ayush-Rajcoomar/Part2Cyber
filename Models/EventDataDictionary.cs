using System;
using System.Collections.Generic;

namespace WPFModernVerticalMenu.Models
{
    public class EventDataDictionary
    {
        public Dictionary<DateTime, List<string>> EventDictionary { get; private set; }

        public EventDataDictionary()
        {
            EventDictionary = new Dictionary<DateTime, List<string>>();
            LoadEvents();
        }

        public void LoadEvents()
        {
            // Hardcoded Events Data
            EventDictionary[new DateTime(2024, 10, 1)] = new List<string>
            {
                ">Sustainability Workshop",
                ">Local Farmers Meetup"
            };

            EventDictionary[new DateTime(2024, 10, 2)] = new List<string>
            {
                ">Renewable Energy Expo",
                ">Agri-Tech Conference"
            };

            EventDictionary[new DateTime(2024, 10, 3)] = new List<string>
            {
                ">Community Garden Planting",
                ">Drone Technology Showcase"
            };

            EventDictionary[new DateTime(2024, 10, 4)] = new List<string>
            {
                ">Urban Farming Seminar",
                ">Water Conservation Talk"
            };

            EventDictionary[new DateTime(2024, 10, 5)] = new List<string>
            {
                ">Sustainable Business Networking",
                ">Organic Market Fair"
            };

            EventDictionary[new DateTime(2024, 10, 6)] = new List<string>
            {
                ">Soil Health Workshop",
                ">AI in Agriculture Webinar"
            };

            EventDictionary[new DateTime(2024, 10, 7)] = new List<string>
            {
                ">Renewable Energy for Farms",
                ">Local Art Exhibition"
            };

            EventDictionary[new DateTime(2024, 10, 8)] = new List<string>
            {
                ">Beekeeping Essentials",
                ">Agri-FinTech Innovations"
            };

            EventDictionary[new DateTime(2024, 10, 9)] = new List<string>
            {
                ">Community Clean-Up Drive",
                ">Food Preservation Workshop"
            };

            EventDictionary[new DateTime(2024, 10, 10)] = new List<string>
            {
                ">Permaculture Principles",
                ">Hydroponics Training"
            };

            EventDictionary[new DateTime(2024, 10, 11)] = new List<string>
            {
                ">Climate Change Discussion",
                ">Tech in Agri-Business Seminar"
            };

            EventDictionary[new DateTime(2024, 10, 12)] = new List<string>
            {
                ">Green Energy Innovations",
                ">Biodiversity Conservation Workshop"
            };

            EventDictionary[new DateTime(2024, 10, 13)] = new List<string>
            {
                ">Farmers' Cooperative Meeting",
                ">Energy-Efficient Farming Practices"
            };

            EventDictionary[new DateTime(2024, 10, 14)] = new List<string>
            {
                ">Aquaponics Training",
                ">Sustainable Living Fair"
            };

            EventDictionary[new DateTime(2024, 10, 15)] = new List<string>
            {
                ">Soil Regeneration Techniques",
                ">Smart Irrigation Systems"
            };

            EventDictionary[new DateTime(2024, 10, 16)] = new List<string>
            {
                ">Urban Agriculture Strategies",
                ">Farm-to-Table Movement Talk"
            };

            EventDictionary[new DateTime(2024, 10, 17)] = new List<string>
            {
                ">Food Waste Reduction Workshop",
                ">Solar Energy in Agriculture"
            };

            EventDictionary[new DateTime(2024, 10, 18)] = new List<string>
            {
                ">Composting Workshop",
                ">Biogas Technology Overview"
            };

            EventDictionary[new DateTime(2024, 10, 19)] = new List<string>
            {
                ">Greenhouse Management",
                ">Sustainable Packaging Seminar"
            };

            EventDictionary[new DateTime(2024, 10, 20)] = new List<string>
            {
                ">Vertical Farming Insights",
                ">Agroforestry Workshop"
            };

            EventDictionary[new DateTime(2024, 10, 21)] = new List<string>
            {
                ">Eco-Friendly Pest Control",
                ">Electric Vehicles for Agriculture"
            };

            EventDictionary[new DateTime(2024, 10, 22)] = new List<string>
            {
                ">Renewable Energy Solutions",
                ">Local Food Producers Market"
            };

            EventDictionary[new DateTime(2024, 10, 23)] = new List<string>
            {
                ">Water Recycling Techniques",
                ">Farmers' Innovation Lab"
            };

            EventDictionary[new DateTime(2024, 10, 24)] = new List<string>
            {
                ">Agricultural Policy Discussion",
                ">Women in Agriculture Panel"
            };

            EventDictionary[new DateTime(2024, 10, 25)] = new List<string>
            {
                ">AI for Crop Monitoring",
                ">Community Seed Exchange"
            };

            EventDictionary[new DateTime(2024, 10, 26)] = new List<string>
            {
                ">Sustainable Fisheries Talk",
                ">Plant-Based Nutrition Seminar"
            };

            EventDictionary[new DateTime(2024, 10, 27)] = new List<string>
            {
                ">Smart Farming Solutions",
                ">Water Harvesting Techniques"
            };

            EventDictionary[new DateTime(2024, 10, 28)] = new List<string>
            {
                ">Electric Tractors Demo",
                ">Farmers' Mental Health Discussion"
            };

            EventDictionary[new DateTime(2024, 10, 29)] = new List<string>
            {
                ">Regenerative Agriculture",
                ">Food Security Strategies"
            };

            EventDictionary[new DateTime(2024, 10, 30)] = new List<string>
            {
                ">Workshop on AI",
                ">Team Building Event"
            };
        }

        public List<string> GetEventsForDate(DateTime date)
        {
            if (EventDictionary.ContainsKey(date))
            {
                return EventDictionary[date];
            }
            return new List<string> { "No events for this date." };
        }

        // Overwrite the events for a specific date with a new event
        public void AddEventForDate(DateTime date, string eventTitle)
        {
            EventDictionary[date] = new List<string> { eventTitle }; // Replace existing events
        }
    }
}
