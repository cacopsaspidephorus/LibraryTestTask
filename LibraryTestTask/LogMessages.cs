namespace LibraryTestTask
{
    public static class LogMessages
    {
        public static string BookAlreadyExists = "A book with the same SKU already exists.";
        public static string BookNotFound = "Book not found.";
        public static string CannotChangeBook = "Cannot change book data as it has been issued to readers.";
        public static string CannotDeleteBook = "Cannot delete book as it has been issued to readers.";
        public static string BookAlreadyIssued = "The book is already issued to the reader.";
        public static string BookIsNotIssued = "The book is not issued to the reader.";

        public static string ReaderAlreadyExists = "A reader with the same name and date of birth already exists.";
        public static string ReaderNotFound = "Reader not found.";
        public static string CannotChangeReader = "Cannot change reader data as they have issued books.";
        public static string CannotDeleteReader = "Cannot delete the reader as they have issued books.";
    }
}
