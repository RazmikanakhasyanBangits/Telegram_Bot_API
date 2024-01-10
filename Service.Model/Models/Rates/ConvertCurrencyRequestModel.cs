using FluentValidation;

namespace Service.Model.Models.Rates;

public class ConvertCurrencyRequestModel
{
    public string From { get; set; }
    public string To { get; set; }
    public decimal Amount { get; set; }
}
public class ConvertCurrencyRequestModelValidator : AbstractValidator<ConvertCurrencyRequestModel>
{
    public ConvertCurrencyRequestModelValidator()
    {
        RuleFor(x => x.Amount)
                       .NotNull()
                       .NotEmpty().WithMessage("Amount Not Specified");
    }
}