using FluentValidation;
using Transacao.Domain.Entities;

namespace Transacao.Domain.Validation
{
    public class TransacaoValidation : AbstractValidator<TransacaoEntity>
    {
        public TransacaoValidation()
        {
            RuleFor(c => c.Usuario)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(10).WithMessage("O campo {PropertyName} precisa tem um valor superior a R$10,00");

        }
    }
}
