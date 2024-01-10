using FluentValidation;

namespace Service.Model.Models.Rates;

public class GetAllRatesRequestModel
{
    public string Currency { get; set; }
    public double Amount { get; set; }
}

public class GetAllRatesRequestModelValidator : AbstractValidator<GetAllRatesRequestModel>
{
    public GetAllRatesRequestModelValidator()
    {
        RuleFor(x => x.Amount)
                      .NotNull()
                      .NotEmpty().WithMessage("Amount Not Specified");

        RuleFor(x => x.Currency)
                      .NotNull()
                      .NotEmpty().WithMessage("Currency Not Specified");

        RuleFor(x => x.Currency)
                      .MaximumLength(3)
                      .MinimumLength(3)
                      .WithMessage("Incorrect Currency Length");
    }
}
