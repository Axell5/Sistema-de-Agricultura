using System;

namespace Agriculture.Data.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public DateTime LastReading { get; set; }
        public bool IsActive { get; set; }
    }
}