namespace CloudBlazor.Models
{
    public class Booking
    {
        public int BookingID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; } = null!;

        public int ClassroomID { get; set; }
        public Classroom Classroom { get; set; } = null!;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Purpose { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? ApprovedBy { get; set; }
        public User? Approver { get; set; }
        public DateTime? ApprovalDate { get; set; }
    }

}
