using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
namespace ScheduleGettingStarted
{
    /// <summary>   
    /// Represents collection of appointments.   
    /// </summary>
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        List<string> eventNameCollection;
        List<Color> colorCollection;
        public ObservableCollection<object> meetingRooms;
        public ObservableCollection<Meeting> meetings;
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime minDate;
        public ObservableCollection<object> MeetingRooms 
        {
            get { return meetingRooms; }
            set
            {
                meetingRooms = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        public ObservableCollection<Meeting> Meetings 
        {
            get { return meetings; }
            set
            {
                meetings = value;
                this.RaiseOnPropertyChanged("Meetings");
            }
        }
        
        public DateTime MinimumDate
        {
            get { return minDate; }
            set
            {
                minDate = value;
                this.RaiseOnPropertyChanged("MinimumDate");
            }
        }

        public SchedulerViewModel()
        {
            Meetings = new ObservableCollection<Meeting>();
            MeetingRooms = new ObservableCollection<object>();
            var date = DateTime.Now.Date;
            MinimumDate = DateTime.Now.Date;
            CreateEventNameCollection();
            CreateColorCollection();
            InitializeResources();
            CreateAppointments();

            AppointmentEditorHelper.Appointments = Meetings;
        }

        private void InitializeResources()
        {
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Jammy", Id = "5001", Color = Color.FromHex("#FF339933"), Capacity = 15, RoomType = "Conference" });
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Tweety", Id = "5002", Color = Color.FromHex("#FF00ABA9"), Capacity = 5, RoomType = "Cabin" });
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Nestle", Id = "5003", Color = Color.FromHex("#FFE671B8"), Capacity = 6, RoomType = "Cabin" });
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Phoneix", Id = "5004", Color = Color.FromHex("#FF1BA1E2"), Capacity = 20, RoomType = "Conference" });
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Mission", Id = "5005", Color = Color.FromHex("#FFD80073"), Capacity = 10, RoomType = "Conference" });
            MeetingRooms.Add(new MeetingRoomInfo() { Name = "Emilia", Id = "5006", Color = Color.FromHex("#FFA2C139"), Capacity = 12, RoomType = "Conference" });
        }


        /// <summary>
        /// Creates meetings and stores in a collection.  
        /// </summary>
        private void CreateAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = this.GettingTimeRanges();

            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-80);
            DateTime dateTo = DateTime.Now.AddDays(80);
            DateTime dateRangeStart = DateTime.Now.AddDays(-70);
            DateTime dateRangeEnd = DateTime.Now.AddDays(70);

            var timeRanges = this.GettingTimeRanges();
            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int i = 0; i < MeetingRooms.Count; i++)
                {
                    var meeting = new Meeting();
                    Point timeRange = timeRanges[randomTime.Next(3)];
                    var x = (int)timeRange.X;
                    var y = (int)timeRange.Y;
                    var subject = this.eventNameCollection[randomTime.Next(11)];
                    var startTime = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(x, y), 0, 0);
                    meeting.From = startTime;
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = subject;

                    meeting.IsAllDay = false;
                    var resources = new ObservableCollection<object>();
                    MeetingRoomInfo room = (MeetingRooms[i] as MeetingRoomInfo);
                    resources.Add(room.Id);
                    meeting.Color = room.Color;
                    meeting.ResourceIdCollection = resources;

                    this.Meetings.Add(meeting);
                }
            }
        }

        /// <summary>  
        /// Creates event names collection.  
        /// </summary>  
        private void CreateEventNameCollection()
        {
            eventNameCollection = new List<string>();
            eventNameCollection.Add("Board Meeting");
            eventNameCollection.Add("Client Meeting");
            eventNameCollection.Add("Customer Meeting");
            eventNameCollection.Add("HR Meeting");
            eventNameCollection.Add("Meeting with Client");
            eventNameCollection.Add("Meeting with Team members");
            eventNameCollection.Add("Sprint planning with Team members");
            eventNameCollection.Add("Training session on C#");
            eventNameCollection.Add("Appraisal meeting");
            eventNameCollection.Add("Meeting with General Manager");
            eventNameCollection.Add("Test report validation");
            eventNameCollection.Add("Bug Automation");
        }

        /// <summary>  
        /// Creates color collection.  
        /// </summary>  
        private void CreateColorCollection()
        {
            colorCollection = new List<Color>();
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFA2C139"));
            colorCollection.Add(Color.FromHex("#FFD80073"));
            colorCollection.Add(Color.FromHex("#FF339933"));
            colorCollection.Add(Color.FromHex("#FFE671B8"));
            colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        /// <summary>
        /// Gets the time ranges.
        /// </summary>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 13));
            randomTimeCollection.Add(new Point(14, 18));
            randomTimeCollection.Add(new Point(15, 17));
            return randomTimeCollection;
        }

        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
