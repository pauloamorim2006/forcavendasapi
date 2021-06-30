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
    [Route("api/v{version:apiVersion}/clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;        

        public ClientesController(IMapper mapper,
                                      IClienteService clienteService,
                                      IUriService uriService,
                                      INotificador notificador,
                                      IUser user) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] PaginationFilter filter)
        {
            var response = await _clienteService.ObterTodos(filter);
            return Ok(ResponseHandler<Cliente, ClienteViewModel>(response, filter));            
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorId(Guid id)
        {
            var cliente = await Obter(id);

            if (cliente == null) return NotFound();

            return cliente;
        }

        [HttpGet("recuperar-quantidade")]
        public async Task<ActionResult<int>> RecuperarQuantidade()
        {
            return await _clienteService.RecuperarQuantidade();
        }

        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel ClienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Adicionar(_mapper.Map<Cliente>(ClienteViewModel, Tratamento));

            return CustomResponse(ClienteViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id, ClienteViewModel ClienteViewModel)
        {
            if (id != ClienteViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(ClienteViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Atualizar(_mapper.Map<Cliente>(ClienteViewModel, Tratamento));

            return CustomResponse(ClienteViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var ClienteViewModel = await Obter(id);

            if (ClienteViewModel == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse(ClienteViewModel);
        }

        private async Task<ClienteViewModel> Obter(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteService.Obter(id));
        }

        private Action<IMappingOperationOptions> Tratamento =>
            opt =>
            {
                opt.AfterMap(
                    (src, cliente) =>
                    {
                        ((Cliente)cliente).Cidade = null;
                    }
                );
            };
    }
}
