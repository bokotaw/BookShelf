using BookShelf.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BookShelf.Services
{
    public static class BookService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Book>();
        }

        public static async Task AddBook(string title, string author,string desc, string genre, bool isFav, bool isRead)
        {
            await Init();
            var image = "default_book.png";
            var book = new Book
            {
                BookTitle = title,
                Author = author,
                Description = desc,
                Image = image,
                Genre = genre,
                IsFavorite = isFav,
                IsRead = isRead
            };
            await db.InsertAsync(book);
        }

        internal static async Task<object> GetBook(int id)
        {
            await Init();
            var book = await db.Table<Book>().FirstOrDefaultAsync(c => c.Id == id);
            return book;
        }

        public static async Task RemoveBook(int id)
        {
            await Init();
            await db.DeleteAsync<Book>(id);
        }
        public static async Task<IEnumerable<Book>> GetBook()
        {
            await Init();
            var book = await db.Table<Book>().ToListAsync();
            return book;
        }
        public static async Task<IEnumerable<Book>> GetFavBook()
        {
            await Init();
            var book = await db.QueryAsync<Book>("Select * From Book WHERE IsFavorite = true");
            return book;
        }
        public static async Task<IEnumerable<Book>> GetReadBook()
        {
            await Init();
            var book = await db.QueryAsync<Book>("Select * From Book WHERE IsRead = true");
            return book;
        }
        public static async Task UpdateBook(Book book)
        {
            await Init();
            if (book.Id != 0)
            {
                await db.UpdateAsync(book);
            }
            else
            {
                await db.InsertAsync(book);
            }
        }
        public static async Task AddToFavorite(Book book)
        {
            await Init();
            await db.ExecuteAsync("UPDATE Book SET IsFavorite = true WHERE Id = ?", book.Id);
 
        }
        public static async Task AddToRead(Book book)
        {
            await Init();
            await db.ExecuteAsync("UPDATE Book SET IsRead = true WHERE Id = ?", book.Id);

        }
    }
}
