namespace LibraryTestTask.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfAddition { get; set; }
        public DateTime DateOfChange { get; set; }
        public string Author { get; set; }
        public string Sku { get; set; }
        public int YearOfPublication { get; set; }
        public int NumberOfCopies { get; set; }

        public ICollection<Reader> Readers { get; set; }
    }
}
