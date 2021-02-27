using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.ViewModels;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/unidades")]
    public class UnidadesController : MainController
    {
        private readonly IUnidadeService _unidadeService;
        private readonly IMapper _mapper;

        public UnidadesController(IMapper mapper,
                                      IUnidadeService unidadeService,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _unidadeService = unidadeService;
        }

        [HttpGet]
        public async Task<IEnumerable<UnidadeViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<UnidadeViewModel>>(await _unidadeService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UnidadeViewModel>> ObterPorId(Guid id)
        {
            var unidade = await Obter(id);

            if (unidade == null) return NotFound();

            return unidade;
        }

        [HttpPost]
        public async Task<ActionResult<UnidadeViewModel>> Adicionar(UnidadeViewModel unidadeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _unidadeService.Adicionar(_mapper.Map<Unidade>(unidadeViewModel));

            return CustomResponse(unidadeViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UnidadeViewModel>> Atualizar(Guid id, UnidadeViewModel unidadeViewModel)
        {
            if (id != unidadeViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(unidadeViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _unidadeService.Atualizar(_mapper.Map<Unidade>(unidadeViewModel));

            return CustomResponse(unidadeViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UnidadeViewModel>> Excluir(Guid id)
        {
            var unidadeViewModel = await Obter(id);

            if (unidadeViewModel == null) return NotFound();

            await _unidadeService.Remover(id);

            return CustomResponse(unidadeViewModel);
        }       

        private async Task<UnidadeViewModel> Obter(Guid id)
        {
            return _mapper.Map<UnidadeViewModel>(await _unidadeService.Obter(id));
        }
    }
}
