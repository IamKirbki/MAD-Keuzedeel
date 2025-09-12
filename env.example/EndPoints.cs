namespace MAD_Keuzedeel.env
{
    public static class EndPoints_Example
    {
        // This endpoint has to get the currently lending books of a user when an authorisation bearer token is provided
        // This endpoint has to be able to handle query parameters ?language=en
        // And this endpoint has to be paginated the /{0} as the limit, so, first 10 of the datbase (as an example) then you'd do the request with /10
        // So the full request would be http://<host>/books/10?language=en
        public const string GetBooksLink = "";
        // This endpoint has to get a specific book by its ID, so /{0} is the book ID
        // So the full request would be http://<host>/book/1 <= assuming the book ID is 1
        public const string GetBookLink = "";
        // This endpoint has to reserve a book for a user, so it has to be a POST request with a body containing the user ID and the book ID
        // So the full request would be http://<host>/reservebook with a body of { "userId": 1, "bookId": 1 } <= assuming the user ID is 1 and the book ID is 1
        // This endpoint has to be authorised with a bearer token
        // The response should be a 200 OK if the reservation was successful
        public const string ReserveBookLink = "";
    }
}
