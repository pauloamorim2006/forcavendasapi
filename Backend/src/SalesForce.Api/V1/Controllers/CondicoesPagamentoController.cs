using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.ViewModels;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesForce.Api.Services;
using SalesForce.Business.Filter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/condicoes-pagamento")]
    public class CondicoesPagamentoController : MainController
    {
        private readonly ICondicaoPagamentoService _condicaoPagamentoService;
        private readonly IMapper _mapper;

        public CondicoesPagamentoController(IMapper mapper,
                                      ICondicaoPagamentoService condicaoPagamentoService,
                                      IUriService uriService,
                                      INotificador notificador,
                                      IUser user) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _condicaoPagamentoService = condicaoPagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] PaginationFilter filter)
        {
            return Ok(ResponseHandler<CondicaoPagamento, CondicaoPagamentoViewModel>(await _condicaoPagamentoService.ObterTodos(filter), filter));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CondicaoPagamentoViewModel>> ObterPorId(Guid id)
        {
            var condicaoPagamento = await Obter(id);

            if (condicaoPagamento == null) return NotFound();

            return condicaoPagamento;
        }

        [HttpPost]
        public async Task<ActionResult<CondicaoPagamentoViewModel>> Adicionar(CondicaoPagamentoViewModel CondicaoPagamentoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _condicaoPagamentoService.Adicionar(_mapper.Map<CondicaoPagamento>(CondicaoPagamentoViewModel));

            return CustomResponse(CondicaoPagamentoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CondicaoPagamentoViewModel>> Atualizar(Guid id, CondicaoPagamentoViewModel CondicaoPagamentoViewModel)
        {
            if (id != CondicaoPagamentoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(CondicaoPagamentoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _condicaoPagamentoService.Atualizar(_mapper.Map<CondicaoPagamento>(CondicaoPagamentoViewModel));

            return CustomResponse(CondicaoPagamentoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CondicaoPagamentoViewModel>> Excluir(Guid id)
        {
            var CondicaoPagamentoViewModel = await Obter(id);

            if (CondicaoPagamentoViewModel == null) return NotFound();

            await _condicaoPagamentoService.Remover(id);

            return CustomResponse(CondicaoPagamentoViewModel);
        }

        private async Task<CondicaoPagamentoViewModel> Obter(Guid id)
        {
            return _mapper.Map<CondicaoPagamentoViewModel>(await _condicaoPagamentoService.Obter(id));
        }
    }
}
