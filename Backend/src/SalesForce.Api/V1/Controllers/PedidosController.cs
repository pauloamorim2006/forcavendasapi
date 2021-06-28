using AutoMapper;
using ERP.Api.Controllers;
using ERP.Api.ViewModels;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using FastReport;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SalesForce.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ERP.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedidos")]
    public class PedidosController : MainController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
       
        public PedidosController(IMapper mapper,
                                      IPedidoService pedidoService,
                                      INotificador notificador,
                                      IUriService uriService,
                                      IUser user,
                                      IWebHostEnvironment env) : base(mapper, uriService, notificador, user)
        {
            _mapper = mapper;
            _pedidoService = pedidoService;
            _env = env;
        }
        
        [HttpGet]
        public async Task<IEnumerable<PedidoViewModel>> ObterTodos(
            [FromQuery] DateTime? DataInicial,
            [FromQuery] DateTime? DataFinal,
            [FromQuery] Guid? ClienteId,
            [FromQuery] int? Status)
        {
            return _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoService.RecuperarTodos(DataInicial, DataFinal, ClienteId, Status));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PedidoViewModel>> ObterPorId(Guid id)
        {
            var pedido = await Recuperar(id);

            if (pedido == null) return NotFound();

            return pedido;
        }

        [HttpGet("recuperar-quantidade")]
        public async Task<ActionResult<int>> RecuperarQuantidade()
        {
            return await _pedidoService.RecuperarQuantidade();
        }

        [HttpPost]
        public async Task<ActionResult<PedidoViewModel>> Adicionar(PedidoViewModel PedidoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _pedidoService.Adicionar(_mapper.Map<Pedido>(PedidoViewModel, Tratamento));

            return CustomResponse(PedidoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PedidoViewModel>> Atualizar(Guid id, PedidoViewModel PedidoViewModel)
        {
            if (id != PedidoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(PedidoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _pedidoService.Atualizar(_mapper.Map<Pedido>(PedidoViewModel, Tratamento));

            return CustomResponse(PedidoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PedidoViewModel>> Excluir(Guid id)
        {
            var PedidoViewModel = await Obter(id);

            if (PedidoViewModel == null) return NotFound();

            await _pedidoService.Remover(id);

            return CustomResponse(PedidoViewModel);
        }

        [HttpPut("{id:guid}/cancelar")]
        public async Task<ActionResult<PedidoViewModel>> Cancelar(Guid id)
        {
            var PedidoViewModel = await Obter(id);

            if (PedidoViewModel == null) return NotFound();

            await _pedidoService.Cancelar(id);

            return CustomResponse(PedidoViewModel);
        }

        [HttpGet("gerar-relatorio")]
        public async Task<ActionResult> GerarRelatorio(
            [FromQuery] DateTime? DataInicial,
            [FromQuery] DateTime? DataFinal,
            [FromQuery] Guid? ClienteId,           
            [FromQuery] int? Status)
        {
            try
            {
                var lista = _mapper.Map<List<PedidoViewModel>>(await _pedidoService.RecuperarTodos(DataInicial, DataFinal, ClienteId, Status));

                var filtro = string.Empty;
                if (DataInicial != null)
                {
                    filtro += $@"Inicial {DataInicial.GetValueOrDefault().ToString("dd/MM/yyyy")};";
                }
                if (DataFinal != null)
                {
                    filtro += $@"Final {DataFinal.GetValueOrDefault().ToString("dd/MM/yyyy")};";
                }
                if (Status != null)
                {
                    var StatusDescricao =
                        Status == 1 ? "Faturado" :
                        Status == 2 ? "Cancelado" :
                        "Aberto";
                    filtro +=
                        $@"Status {StatusDescricao};";
                }
                if ((ClienteId != null) && (ClienteId.HasValue))
                {
                    filtro +=
                        $@"Cód. Cliente {ClienteId};";
                }

                Report report = new Report();
                report.Load($@"{_env.ContentRootPath}\Report\Pedidos.frx");
                report.RegisterData(lista, "Pedidos");
                report.SetParameterValue("FILTRO", filtro);
                report.Prepare();

                var pdfExport = new PDFSimpleExport();
                var nomeArquivoPdf = Path.ChangeExtension(Path.GetTempFileName(), "pdf");
                pdfExport.Export(report, nomeArquivoPdf);

                var stream = System.IO.File.OpenRead(nomeArquivoPdf);
                return new FileStreamResult(stream, "application/octet-stream");
            }
            catch (Exception ex)
            {
                return CustomResponse(ex.Message);
            }
        }

        [HttpGet("recuperar-mes")]
        public async Task<List<double>> RecuperarPorMes()
        {
            return await _pedidoService.RecuperarPorMes();
        }

        private async Task<PedidoViewModel> Recuperar(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoService.RecuperarPorId(id));
        }

        private async Task<PedidoViewModel> Obter(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoService.Obter(id));
        }
        private Action<IMappingOperationOptions> Tratamento =>
            opt =>
            {
                opt.AfterMap(
                    (src, pedido) =>
                    {
                        ((Pedido)pedido).CondicaoPagamento = null;
                        ((Pedido)pedido).FormaPagamento = null;
                        ((Pedido)pedido).Cliente = null;
                        foreach (PedidoItem item in ((Pedido)pedido).PedidoItens)
                        {
                            item.Produto = null;
                        }
                    }
                );
            };        
    }
}
