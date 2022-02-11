using SQLite;

namespace BookShelf.Models
{
    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Genre { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRead { get; set; }
    }
}
