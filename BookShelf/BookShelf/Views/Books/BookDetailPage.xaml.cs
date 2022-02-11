using BookShelf.Services;
using System;
using Xamarin.Forms;

namespace BookShelf.Views.Books
{

    [QueryProperty(nameof(BookId), nameof(BookId))]
    public partial class BookDetailPage : ContentPage
    {
        public string BookId { get; set; }
        public BookDetailPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(BookId, out var result);
            BindingContext = await BookService.GetBook(result);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }

}