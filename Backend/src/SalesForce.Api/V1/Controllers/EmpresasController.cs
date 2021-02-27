using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.ViewModels;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/empresas")]
    public class EmpresasController : MainController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresasController(IEmpresaService empresaService,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _empresaService = empresaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<EmpresaViewModel> ObterTodos()
        {
            var registro = _mapper.Map<EmpresaViewModel>(await _empresaService.Buscar());
            return registro;
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaViewModel>> Adicionar(EmpresaViewModel EmpresaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var empresaExistente = await _empresaService.Buscar();
            var empresa = _mapper.Map<Empresa>(EmpresaViewModel);
            if (empresaExistente != null)
            {
                empresa.Id = empresaExistente.Id;
                await _empresaService.Atualizar(empresa);
            }
            else
            {
                await _empresaService.Adicionar(empresa);
            }

            return CustomResponse(empresa);
        }
    }
}
