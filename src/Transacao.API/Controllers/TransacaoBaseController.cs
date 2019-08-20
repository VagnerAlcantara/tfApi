using Microsoft.AspNetCore.Mvc;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    public abstract class TransacaoBaseController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected TransacaoBaseController(INotificador notificador)
        {
            _notificador = notificador;
        }
        protected ActionResult Response(bool success, object result)
        {
            if (success)
                return Ok(new { success = success, data = result });

            return BadRequest(new { success = success, errors = result });
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}