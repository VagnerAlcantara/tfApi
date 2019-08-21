using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    public abstract class TransacaoBaseController : ControllerBase
    {
        private readonly INotificador _notificador;
        internal readonly ILoggerFactory _loggerFactory;

        protected TransacaoBaseController(INotificador notificador, ILoggerFactory loggerFactory)
        {
            _notificador = notificador;
            _loggerFactory = loggerFactory;
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