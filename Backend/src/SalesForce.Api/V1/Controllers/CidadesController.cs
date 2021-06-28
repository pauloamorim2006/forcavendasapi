using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.ViewModels;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesForce.Api.Helpers;
using SalesForce.Api.Services;
using SalesForce.Api.Wrappers;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cidades")]
    public class CidadesController : MainController
    {
        private readonly ICidadeService _cidadeService;
        private readonly IMapper _mapper;

        public CidadesController(IMapper mapper,
                                      ICidadeService cidadeService,
                                      IUriService uriService,
                                      INotificador notificador,
                                      IUser user) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] PaginationFilter filter)
        {            
            return Ok(ResponseHandler<Cidade, CidadeViewModel>(await _cidadeService.ObterTodos(filter), filter));
        }        

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> ObterPorId(Guid id)
        {
            var cidade = await Obter(id);

            if (cidade == null) return NotFound();

            return cidade;
        }

        [HttpPost]
        public async Task<ActionResult<CidadeViewModel>> Adicionar(CidadeViewModel CidadeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _cidadeService.Adicionar(_mapper.Map<Cidade>(CidadeViewModel));

            return CustomResponse(CidadeViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> Atualizar(Guid id, CidadeViewModel CidadeViewModel)
        {
            if (id != CidadeViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(CidadeViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _cidadeService.Atualizar(_mapper.Map<Cidade>(CidadeViewModel));

            return CustomResponse(CidadeViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> Excluir(Guid id)
        {
            var CidadeViewModel = await Obter(id);

            if (CidadeViewModel == null) return NotFound();

            await _cidadeService.Remover(id);

            return CustomResponse(CidadeViewModel);
        }

        private async Task<CidadeViewModel> Obter(Guid id)
        {
            return _mapper.Map<CidadeViewModel>(await _cidadeService.Obter(id));
        }
    }
}
