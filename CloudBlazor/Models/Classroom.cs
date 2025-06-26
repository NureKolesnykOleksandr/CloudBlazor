namespace CloudBlazor.Models
{
    public class Classroom
    {
        public int ClassroomID { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int Capacity { get; set; }
        public string Building { get; set; } = null!;
        public int Floor { get; set; }
        public bool HasProjector { get; set; } = false;
        public bool HasWhiteboard { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }

}
