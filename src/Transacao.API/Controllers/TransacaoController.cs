using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transacao.API.Commands;
using Transacao.Application.Interfaces;
using Transacao.Domain.Entities;
using Transacao.Domain.Interfaces;

namespace Transacao.API.Controllers
{
    [Route("api/v1/transacao")]
    [ApiController]
    public class TransacaoController : TransacaoBaseController, IDisposable
    {
        private readonly ITransacaoAppService _transacaoAppService;
        private readonly IMapper _mapper;
        private readonly INotificador _notificador;
        public TransacaoController(ITransacaoAppService transacaoAppService, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _transacaoAppService = transacaoAppService;
            _mapper = mapper;
            _notificador = notificador;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Response(true, _mapper.Map<List<TransacaoResponse>>(await _transacaoAppService.ObterTodos()));
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
                var response = await _transacaoAppService.ObterPorId(id);

                if (response == null)
                    return NotFound();

                return Response(true, _mapper.Map<TransacaoResponse>(response));
            }
            catch (Exception ex)
            {
                return Response(false, ex);
            }
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]TransacaoInclusaoRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return Response(false, new BadRequestObjectResult(ModelState.Values));

                var transacaoEntity = _mapper.Map<TransacaoEntity>(request);

                await _transacaoAppService.Adicionar(transacaoEntity);

                if (!OperacaoValida())
                    return Response(false, _notificador.ObterNotificacoes());

                return Response(true, _mapper.Map<TransacaoResponse>(transacaoEntity));
            }
            catch (Exception ex)
            {
                return Response(false, ex);
            }
        }

        public void Dispose()
        {
            _transacaoAppService?.Dispose();
        }
    }
}