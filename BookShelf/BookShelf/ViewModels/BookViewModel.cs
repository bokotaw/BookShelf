using BookShelf.Models;
using BookShelf.Services;
using BookShelf.Views;
using BookShelf.Views.Books;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace BookShelf.ViewModels
{
    public class BookViewModel : ViewBaseModel
    {
        public ObservableRangeCollection<Book> Book { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand RefreshFavCommand { get; }
        public AsyncCommand RefreshReadCommand { get; }
        public AsyncCommand<Book> FavoriteCommand { get; }
        public AsyncCommand<Book> ReadCommand { get; }
        public AsyncCommand<Book> SelectedCommand { get; }
        public AsyncCommand<Book> RemoveCommand { get; }
        public AsyncCommand AddCommand { get; }
        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get;}

        public BookViewModel()
        {

            Title = "Moja Biblioteka";
            Book = new ObservableRangeCollection<Book>();

            FavoriteCommand = new AsyncCommand<Book>(Favorite);
            ReadCommand = new AsyncCommand<Book>(Read);
            RefreshCommand = new AsyncCommand(Refresh);
            RefreshFavCommand = new AsyncCommand(FavRefresh);
            RefreshReadCommand = new AsyncCommand(ReadRefresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Book>(Remove);
            SelectedCommand = new AsyncCommand<Book>(Selected);
        }
        async Task Add()
        {
            var route = $"{nameof(AddBookPage)}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Book book)
        {
            await Application.Current.MainPage.DisplayAlert("Usuwanie książki", "Książka została usunięta z biblioteki?","OK");
            await BookService.RemoveBook(book.Id);
            await Refresh();
        }
        async Task Favorite(Book book)
        {
            if (book == null)
                return;
            else
            {
                await BookService.AddToFavorite(book);
                await Application.Current.MainPage.DisplayAlert("Dodano do ulubionych", book.BookTitle, "OK");
            }

        }
        async Task Read(Book book)
        {
            if (book == null)
                return;
            else
            {
                await BookService.AddToRead(book);
                await Application.Current.MainPage.DisplayAlert("Dodano do przeczytanych", book.BookTitle, "OK");
            }

        }

        async Task Selected(Book book)
        {
            if (book == null)
                return;

            var route = $"{nameof(BookDetailPage)}?BookId={book.Id}";
            await Shell.Current.GoToAsync(route);
        }
        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Book.Clear();
            var books = await BookService.GetBook();
            Book.AddRange(books);
            IsBusy = false;
        }
        async Task FavRefresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Book.Clear();
            var books = await BookService.GetFavBook();
            Book.AddRange(books);
            IsBusy = false;
        }
        async Task ReadRefresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Book.Clear();
            var books = await BookService.GetReadBook();
            Book.AddRange(books);
            IsBusy = false;
        }
        void DelayLoadMore()
        {
            if (Book.Count <= 10)
                return;
        }

    }
}
