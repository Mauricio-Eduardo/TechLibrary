using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Books.Filter
{
    public class FilterBooksValidator : AbstractValidator<RequestFilterBooksJson>
    {
        public FilterBooksValidator()
        {
            RuleFor(request => request.PageNumber).GreaterThan(0).WithMessage("O número da página é obrigatório!");
        }
    }
}
