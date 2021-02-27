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
    public class CidadeService : BaseService, ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        public CidadeService(ICidadeRepository cidadeRepository,
                                 INotificador notificador) : base(notificador)
        {
            _cidadeRepository = cidadeRepository;
        }
        public async Task<bool> Adicionar(Cidade cidade)
        {
            if (!ExecutarValidacao(new CidadeValidation(), cidade)) return false;

            if (_cidadeRepository.JaExiste(cidade.Id, cidade.CodigoIbge))
            {
                Notificar("Já existe uma cidade com este código do IBGE informado.");
                return false;
            }

            await _cidadeRepository.Adicionar(cidade);
            return true;
        }
        public async Task<bool> Atualizar(Cidade cidade)
        {
            if (!ExecutarValidacao(new CidadeValidation(), cidade)) return false;

            if (_cidadeRepository.JaExiste(cidade.Id, cidade.CodigoIbge))
            {
                Notificar("Já existe uma cidade com este código do IBGE informado.");
                return false;
            }

            await _cidadeRepository.Atualizar(cidade);
            return true;
        }
        public async Task<bool> Remover(Guid id)
        {
            if (!_cidadeRepository.Encontrou(id))
            {
                Notificar("Já existe uma cidade com este documento informado.");
                return false;
            }
            await _cidadeRepository.Remover(id);
            return true;
        }
        public async Task<IEnumerable<Cidade>> Buscar(Expression<Func<Cidade, bool>> predicate)
        {
            return await _cidadeRepository.Buscar(predicate);
        }
        public async Task<Cidade> ObterPorId(Guid id)
        {
            return await _cidadeRepository.ObterPorId(id);
        }
        public async Task<Cidade> Obter(Guid id)
        {
            return await _cidadeRepository.Obter(id);
        }
        public async Task<List<Cidade>> ObterTodos()
        {
            var cidades = await _cidadeRepository.ObterTodos();
            return cidades.OrderBy(x => x.Descricao).ToList();
        }

        public void Dispose()
        {
            _cidadeRepository?.Dispose();
        }
    }
}
