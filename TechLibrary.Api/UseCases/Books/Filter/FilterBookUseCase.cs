using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Books.Filter
{
    public class FilterBookUseCase
    {
        public ResponseBooksJson Execute(RequestFilterBooksJson request)
        {
            var dbContext = new TechLibraryDbContext();

            return new ResponseBooksJson
            {

            };
        }
    }
}
