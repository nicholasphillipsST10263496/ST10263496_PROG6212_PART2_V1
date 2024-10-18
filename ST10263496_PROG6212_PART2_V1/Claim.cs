using System;
namespace ST10263496_PROG6212_PART2_V1.Models
{
    public class Claim
    {
        // Getters and Setters for the Claim properties
        public int ClaimId { get; set; } // Primary Key
        public string LecturerName { get; set; }
        public int HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Status { get; set; }
        public string DocumentPath { get; set; }
    }
}
