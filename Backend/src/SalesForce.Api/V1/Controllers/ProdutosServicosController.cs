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
    [Route("api/v{version:apiVersion}/produtos-servicos")]
    public class ProdutosServicosController : MainController
    {
        private readonly IProdutoServicoService _produtoServicoService;
        private readonly IMapper _mapper;

        public ProdutosServicosController(IMapper mapper,
                                      IProdutoServicoService produtoServicoService,
                                      IUriService uriService,
                                      INotificador notificador,
                                      IUser user) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _produtoServicoService = produtoServicoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] PaginationFilter filter)
        {            
            return Ok(ResponseHandler<ProdutoServico, ProdutoServicoViewModel>(await _produtoServicoService.ObterTodos(filter), filter));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoServicoViewModel>> ObterPorId(Guid id)
        {
            var produtoServico = await Obter(id);

            if (produtoServico == null) return NotFound();

            return _mapper.Map<ProdutoServicoViewModel>(produtoServico);
        }

        [HttpGet("recuperar-quantidade")]
        public async Task<ActionResult<int>> RecuperarQuantidade()
        {
            return await _produtoServicoService.RecuperarQuantidade();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoServicoViewModel>> Adicionar(ProdutoServicoViewModel produtoServicoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _produtoServicoService.Adicionar(_mapper.Map<ProdutoServico>(produtoServicoViewModel, Tratamento));

            return CustomResponse(produtoServicoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProdutoServicoViewModel>> Atualizar(Guid id, ProdutoServicoViewModel produtoServicoViewModel)
        {
            if (id != produtoServicoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(produtoServicoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _produtoServicoService.Atualizar(_mapper.Map<ProdutoServico>(produtoServicoViewModel, Tratamento));

            return CustomResponse(produtoServicoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoServicoViewModel>> Excluir(Guid id)
        {
            var produtoServicoViewModel = await Obter(id);

            if (produtoServicoViewModel == null) return NotFound();

            await _produtoServicoService.Remover(id);

            return CustomResponse(produtoServicoViewModel);
        }

        private async Task<ProdutoServicoViewModel> Obter(Guid id)
        {
            return _mapper.Map<ProdutoServicoViewModel>(await _produtoServicoService.Obter(id));
        }

        private Action<IMappingOperationOptions> Tratamento =>
            opt =>
            {
                opt.AfterMap(
                    (src, produtoServico) =>
                    {
                        ((ProdutoServico)produtoServico).Unidade = null;
                    }
                );
            };        
    }
}
