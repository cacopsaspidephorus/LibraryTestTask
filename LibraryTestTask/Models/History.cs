namespace LibraryTestTask.Models
{
    public class History
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}