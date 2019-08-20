using FluentValidation;
using Transacao.Domain.Entities;

namespace Transacao.Domain.Validation
{
    public class ContaValidation : AbstractValidator<ContaEntity>
    {
        public ContaValidation()
        {
            RuleFor(c => c.Agencia)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Numero)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Digito)
               .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
