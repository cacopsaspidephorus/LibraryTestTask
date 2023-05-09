namespace LibraryTestTask.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfAddition { get; set; }
        public DateTime DateOfChange { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> IssuedBooks { get; set; }
    }
}
