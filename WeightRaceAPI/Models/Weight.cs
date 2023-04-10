using System;
using System.Collections.Generic;

namespace WeightRaceAPI.Models
{
    public class Weight
    {
        public int WeightId { get; set; }
        public DateTime LogDate { get; set; }
        public double Value { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
