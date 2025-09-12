using MAD_Keuzedeel.env;
using MAD_Keuzedeel.Models;
using MAD_Keuzedeel.Models.Response;
using MAD_Keuzedeel.Utils;
using System.Net.Http.Json;

namespace MAD_Keuzedeel.Services
{
    public class BookService
    {
        public async Task<List<Book>> GetBooks(int offset, Dictionary<string, string> queryParams = null)
        {
            Uri uri = new(string.Format(EndPoints.GetBooksLink, offset) + ApiUtils.CreateQueryString(queryParams));
            return await ApiUtils.FetchAndDeserialize<List<Book>, BooksResponse>(uri, "data")
                ?? new List<Book>();
        }

        public async Task<Book> GetBook(Guid UUID)
        {
            Uri uri = new(string.Format(EndPoints.GetBookLink, UUID));
            return await ApiUtils.FetchAndDeserialize<Book, BookResponse>(uri, "data")
                ?? new Book();
        }

        public async Task<List<Book>> GetReservedBooks(int offset)
        {
            Uri uri = new(string.Format(EndPoints.GetBooksLink, offset));
            return await ApiUtils.FetchAndDeserialize<List<Book>, BooksResponse>(uri, "data", MauiProgram.AuthService._AUTH.access_token) 
                ?? new List<Book>();
        }

        public async Task<bool> ReserveBook(Book book)
        {
            using HttpClient _httpClient = new();
            var response = await _httpClient.PostAsJsonAsync(
                EndPoints.ReserveBookLink,
                new { userId = MauiProgram.AuthService._USER.id, bookId = book.id }
            );
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}