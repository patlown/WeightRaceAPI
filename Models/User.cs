using System;
using System.Collections.Generic;

namespace WeightRaceAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserUid { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DOB { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public double StartWeight { get; set; }
        public double? DayChange { get; set; }
        public double? WeekChange { get; set; }
        public double? TotalChange { get; set; }

        public List<Weight>? Weights { get; set; }
    }
}
