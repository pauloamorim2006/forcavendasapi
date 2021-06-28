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
    [Route("api/v{version:apiVersion}/formas-pagamento")]
    public class FormasPagamentoController : MainController
    {
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IMapper _mapper;

        public FormasPagamentoController(IMapper mapper,
                                      IFormaPagamentoService formaPagamentoService,
                                      IUriService uriService,
                                      INotificador notificador,
                                      IUser user) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _formaPagamentoService = formaPagamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] PaginationFilter filter)
        {
            return Ok(ResponseHandler<FormaPagamento, FormaPagamentoViewModel>(await _formaPagamentoService.ObterTodos(filter), filter));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> ObterPorId(Guid id)
        {
            var formaPagamento = await Obter(id);

            if (formaPagamento == null) return NotFound();

            return formaPagamento;
        }

        [HttpPost]
        public async Task<ActionResult<FormaPagamentoViewModel>> Adicionar(FormaPagamentoViewModel FormaPagamentoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _formaPagamentoService.Adicionar(_mapper.Map<FormaPagamento>(FormaPagamentoViewModel));

            return CustomResponse(FormaPagamentoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> Atualizar(Guid id, FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (id != formaPagamentoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(formaPagamentoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _formaPagamentoService.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoViewModel));

            return CustomResponse(formaPagamentoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> Excluir(Guid id)
        {
            var formaPagamentoViewModel = await Obter(id);

            if (formaPagamentoViewModel == null) return NotFound();

            await _formaPagamentoService.Remover(id);

            return CustomResponse(formaPagamentoViewModel);
        }

        private async Task<FormaPagamentoViewModel> Obter(Guid id)
        {
            return _mapper.Map<FormaPagamentoViewModel>(await _formaPagamentoService.Obter(id));
        }
    }
}
