using MAD_Keuzedeel.Models;
using MAD_Keuzedeel.Services;
using System.Diagnostics;

namespace MAD_Keuzedeel.Pages.MainPage
{
    public partial class View : ContentPage
    {
        readonly BookService _bookService = new();
        int count = 0;

        public View()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await MauiProgram.AuthService.InitializeAuthorisation();
            List<Book> data = await _bookService.GetBooks(0);
            Debug.WriteLine(data);
            Book book = await _bookService.GetBook(data[0].id);
            await _bookService.ReserveBook(data[0]);
        }
    }
}
