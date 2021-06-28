using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ERP.Business.Intefaces;
using ERP.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SalesForce.Api.Helpers;
using SalesForce.Api.Services;
using SalesForce.Api.Wrappers;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;

namespace ERP.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;
        public readonly IUser AppUser;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected MainController(
                                 IMapper mapper,
                                 IUriService uriService,
                                 INotificador notificador, 
                                 IUser appUser)
        {
            _notificador = notificador;
            _mapper = mapper;
            _uriService = uriService;
            AppUser = appUser;
            
            if (appUser.IsAuthenticated())
            {
                UsuarioId = appUser.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected PagedResponse<List<ViewModel>> ResponseHandler<Model, ViewModel>(ResponseModel<Model> response, PaginationFilter filter)
        {
            var data = _mapper.Map<IEnumerable<ViewModel>>(response.Data);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ViewModel>(
                data.ToList(), 
                new PaginationFilter(filter.PageNumber, filter.PageSize), 
                response.Total, 
                _uriService, 
                Request.Path.Value);
            return pagedReponse;
        }

    }
}
