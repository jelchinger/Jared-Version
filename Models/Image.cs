using System;

namespace Angular2.Models
{
    public class Image
    {
        
        public string info { get; set; }
        public char GPSLatitudeRef { get; set; }
        
        public string GPSLatitude { get; set;}

        public int LatitudeDegree { get; set; }

        public int LatitudeMinute { get; set; }

        public double LatitudeSecond { get; set; }
        
        public char GPSLongitudeRef { get; set; }

        public string GPSLongitude { get; set; }

        public int LongitudeDegree { get; set; }

        public int LongitudeMinute { get; set; }

        public double LongitudeSecond { get; set; }

        public DateTime dateTime { get; set; }

    }
}