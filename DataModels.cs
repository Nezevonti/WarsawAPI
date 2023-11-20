using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarsawAPI
{
    internal class GeographicalCoordinates
    {
        public GeographicalCoordinates()
        {
            latitude = 0.0; longitude = 0.0;
        }
        public GeographicalCoordinates(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    //zespół, rozróżniany po stopID
    class TransitSet
    {
        public string stopID { get; set; }
        public string stopName { get; set; }

        public List<TransitStop> stopList { get; set; }
    }

    //słupek
    class TransitStop
    {
        public string stopNr { get; set; }

        public string[]? transitLinesNumbers { get; set; }

        public List<LineSchedule> lineSchedules { get; set; }
    }

    class LineSchedule
    {

        public string lineNr { get; set; }
        public List<LineScheduleTime> lineScheduleTimes { get; set; }

        public LineSchedule(string lineNr)
        {
            lineScheduleTimes = new List<LineScheduleTime>();
            this.lineNr = lineNr;
        }
    }

    class LineScheduleTime
    {
        public string direction { get; set; }
        public string route { get; set; }

        public TimeSpan? time { get; set; }
    }

}
