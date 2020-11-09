using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ScheduleGettingStarted
{
    /// <summary>   
    /// Represents custom data properties.   
    /// </summary> 
    public class MeetingRoomInfo
    {
        public string Name { get; set; }
        public object Id { get; set; }
        public Color Color { get; set; }
        public int Capacity { get; set; }

        public string RoomType { get; set; }
    }

}
