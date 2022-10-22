using FluentValidation;
using Maffei.API.DataModels;
using Maffei.API.Dtos;

namespace Maffei.API.FluentValidation
{
    public class AboneValidator: AbstractValidator<AddAboneDto>
    {
        public AboneValidator()
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage("Lütfen Abone Numaranızı Boş Geçmeyiniz");
            RuleFor(x => x.Number).MaximumLength(8).MinimumLength(8).WithMessage("Abone Numarası 8 Karakterden Oluşmalıdır.");
            
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen Adınızı Boş Geçmeyiniz");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen Soyadınızı Boş Geçmeyiniz");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Lütfen Adres Alanınızı Boş Geçmeyiniz");
            RuleFor(x => x.Address).MaximumLength(100).WithMessage("Adres Alanı 100 karakterden fazla olamaz");
            RuleFor(x => x.CurrencyUnitId).NotEmpty();
            RuleFor(x => x.KdvId).NotEmpty();
           
        }
    }
}
