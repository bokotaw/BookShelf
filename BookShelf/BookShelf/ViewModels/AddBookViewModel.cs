using BookShelf.Services;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{
    [QueryProperty(nameof(BookTitle), nameof(BookTitle))]
    public class AddBookViewModel : ViewBaseModel
    {
        string bookTitle, author, desc, genre;
        bool isFav, isRead;  
        public string BookTitle { get { return bookTitle; } set { SetProperty(ref bookTitle, value); } }
        public string Author { get { return author; } set { SetProperty(ref author, value); } }
        public string Desc { get { return desc; } set { SetProperty(ref desc, value); } }
        public string Genre { get { return genre; } set { SetProperty(ref genre, value); } }
        public bool IsFavorite { get { return isFav; } set { SetProperty(ref isFav, value); } }
        public bool IsRead { get { return isRead; } set { SetProperty(ref isRead, value); } }
        public AsyncCommand SaveCommand { get; }
        public AddBookViewModel()
        {
            Title = "Dodaj książkę";
            SaveCommand = new AsyncCommand(Save);
        }
        async Task Save()
        {
          
            if (string.IsNullOrWhiteSpace(BookTitle) ||
                string.IsNullOrWhiteSpace(Author) ||
                string.IsNullOrWhiteSpace(Desc) ||
                string.IsNullOrWhiteSpace(Genre))
            {
                return;
            }
            await BookService.AddBook(bookTitle, author, desc, genre, isFav, isRead);

            await Shell.Current.GoToAsync("..");
        }
    }
}
