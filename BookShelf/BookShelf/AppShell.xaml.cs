using BookShelf.Views;
using BookShelf.Views.Books;
using Xamarin.Forms;

namespace BookShelf
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddBookPage),
               typeof(AddBookPage));
            Routing.RegisterRoute(nameof(BookDetailPage),
                typeof(BookDetailPage));
        }

    }
}
