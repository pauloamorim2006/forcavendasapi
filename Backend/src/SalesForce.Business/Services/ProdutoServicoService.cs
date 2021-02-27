using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Services
{
    public class ProdutoServicoService : BaseService, IProdutoServicoService
    {
        private readonly IProdutoServicoRepository _produtoServicoRepository;

        public ProdutoServicoService(IProdutoServicoRepository produtoServicoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _produtoServicoRepository = produtoServicoRepository;
        }

        public async Task<bool> Adicionar(ProdutoServico produtoServico)
        {
            if (!ExecutarValidacao(new ProdutoServicoValidation(), produtoServico)) return false;

            if (_produtoServicoRepository.JaExiste(produtoServico.Id, produtoServico.Nome))
            {
                Notificar("Já existe um produto com este nome informado.");
                return false;
            }

            await _produtoServicoRepository.Adicionar(produtoServico);
            return true;
        }
        public async Task<bool> Atualizar(ProdutoServico produtoServico)
        {
            if (!ExecutarValidacao(new ProdutoServicoValidation(), produtoServico)) return false;

            if (_produtoServicoRepository.JaExiste(produtoServico.Id, produtoServico.Nome))
            {
                Notificar("Já existe um produto com este nome informado.");
                return false;
            }

            await _produtoServicoRepository.Atualizar(produtoServico);
            return true;
        }
        public async Task<bool> Remover(Guid id)
        {
            if (!_produtoServicoRepository.Encontrou(id))
            {
                Notificar("Nenhum produto/serviço encontrado.");
                return false;
            }
            await _produtoServicoRepository.Remover(id);
            return true;
        }
        public async Task<IEnumerable<ProdutoServico>> Buscar(Expression<Func<ProdutoServico, bool>> predicate)
        {
            return await _produtoServicoRepository.Buscar(predicate);
        }
        public async Task<ProdutoServico> ObterPorId(Guid id)
        {
            return await _produtoServicoRepository.ObterPorId(id);
        }
        public async Task<ProdutoServico> Obter(Guid id)
        {
            return await _produtoServicoRepository.Obter(id);
        }
        public async Task<List<ProdutoServico>> ObterTodos()
        {
            return await _produtoServicoRepository.ObterTodos();
        }
        public async Task<List<ProdutoServico>> RecuperarTodos()
        {
            return await _produtoServicoRepository.RecuperarTodos();
        }
        public async Task<int> RecuperarQuantidade()
        {
            return await _produtoServicoRepository.RecuperarQuantidade();
        }

        public void Dispose()
        {
            _produtoServicoRepository?.Dispose();
        }
    }
}
