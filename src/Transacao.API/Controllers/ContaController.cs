using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transacao.API.Commands;
using Transacao.Application.Interfaces;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    [Route("api/v1/conta")]
    [ApiController]
    public class ContaController : TransacaoBaseController, IDisposable
    {
        private readonly IContaAppService _contaAppService;
        private readonly INotificador _notificador;
        private readonly IMapper _mapper;
        public ContaController(IContaAppService contaAppService, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _contaAppService = contaAppService;
            _mapper = mapper;
            _notificador = notificador;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Response(true, _mapper.Map<List<ContaResponse>>(await _contaAppService.ObterTodos()));
            }
            catch (Exception ex)
            {
                return Response(false, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var response = await _contaAppService.ObterPorId(id);

                if (response == null)
                    return NotFound();

                return Response(true, _mapper.Map<ContaResponse>(response));
            }
            catch (Exception ex)
            {
                return Response(false, ex);
            }
        }

        public void Dispose()
        {
            _contaAppService?.Dispose();
        }
    }
}