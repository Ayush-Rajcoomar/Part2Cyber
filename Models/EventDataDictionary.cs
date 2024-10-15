using System.Collections.Generic;
using System;
using System.Linq;

public class EventDataDictionary
{
    private Dictionary<DateTime, HashSet<string>> events;

    public EventDataDictionary()
    {
        events = new Dictionary<DateTime, HashSet<string>>();
        
    }

   

    public HashSet<string> GetEventsForDate(DateTime date)
    {
        return events.ContainsKey(date) ? events[date] : new HashSet<string>();
    }

    public IEnumerable<HashSet<string>> GetAllEvents()
    {
        return events.Values;
    }

    public void AddEventForDate(DateTime date, string eventTitle)
    {
        if (!events.ContainsKey(date))
        {
            events[date] = new HashSet<string>();
        }

        if (!events[date].Contains(eventTitle))  // This prevents duplicate event titles
        {
            events[date].Add(eventTitle);
        }
    }

    
}
