using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
namespace ScheduleGettingStarted
{
    /// <summary>   
    /// Represents custom data properties.   
    /// </summary>  
    public class Meeting
    {
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string ContactID { get; set; }
        public int Capacity { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool IsAllDay { get; set; }
        public Color Color { get; set; }
        public ObservableCollection<object> ResourceIdCollection { get; set; }
        public string RecurrenceRule { get; set; }
    }
}
