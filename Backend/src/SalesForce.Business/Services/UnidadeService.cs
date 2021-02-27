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
    public class UnidadeService : BaseService, IUnidadeService
    {
        private readonly IUnidadeRepository _unidadeRepository;

        public UnidadeService(IUnidadeRepository unidadeRepository,
                                 INotificador notificador) : base(notificador)
        {
            _unidadeRepository = unidadeRepository;
        }

        public async Task<bool> Adicionar(Unidade unidade)
        {
            if (!ExecutarValidacao(new UnidadeValidation(), unidade)) return false;

            if (_unidadeRepository.JaExiste(unidade.Id, unidade.Sigla))
            {
                Notificar("Já existe uma unidade com esta unidade informada.");
                return false;
            }

            await _unidadeRepository.Adicionar(unidade);
            return true;
        }

        public async Task<bool> Atualizar(Unidade unidade)
        {
            if (!ExecutarValidacao(new UnidadeValidation(), unidade)) return false;

            if (_unidadeRepository.JaExiste(unidade.Id, unidade.Sigla))
            {
                Notificar("Já existe uma unidade com esta unidade informada.");
                return false;
            }

            await _unidadeRepository.Atualizar(unidade);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!_unidadeRepository.Encontrou(id))
            {
                Notificar("Já existe uma unidade de medida com está sigla informado.");
                return false;
            }

            await _unidadeRepository.Remover(id);
            return true;
        }

        public async Task<IEnumerable<Unidade>> Buscar(Expression<Func<Unidade, bool>> predicate)
        {
            return await _unidadeRepository.Buscar(predicate);
        }

        public async Task<Unidade> ObterPorId(Guid id)
        {
            return await _unidadeRepository.ObterPorId(id);
        }

        public async Task<Unidade> Obter(Guid id)
        {
            return await _unidadeRepository.Obter(id);
        }

        public async Task<List<Unidade>> ObterTodos()
        {
            return await _unidadeRepository.ObterTodos();
        }

        public async Task<List<Unidade>> RecuperarTodos()
        {
            return await _unidadeRepository.RecuperarTodos();
        }

        public void Dispose()
        {
            _unidadeRepository?.Dispose();
        }
    }
}
