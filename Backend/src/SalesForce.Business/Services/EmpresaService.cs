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
    public class EmpresaService : BaseService, IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository,
                                 INotificador notificador) : base(notificador)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<bool> Adicionar(Empresa empresa)
        {
            if (!ExecutarValidacao(new EmpresaValidation(), empresa)) return false;

            if (_empresaRepository.JaExiste(empresa.Id, empresa.CnpjCpfDi))
            {
                Notificar("Já existe uma empresa com este CNPJ/CPF/DI informado.");
                return false;
            }

            await _empresaRepository.Adicionar(empresa);
            return true;
        }
        public async Task<bool> Atualizar(Empresa empresa)
        {
            if (!ExecutarValidacao(new EmpresaValidation(), empresa)) return false;

            if (_empresaRepository.JaExiste(empresa.Id, empresa.CnpjCpfDi))
            {
                Notificar("Já existe uma empresa com este CNPJ/CPF/DI informado.");
                return false;
            }

            await _empresaRepository.Atualizar(empresa);
            return true;
        }
        public async Task<bool> Remover(Guid id)
        {
            if (!_empresaRepository.Encontrou(id))
            {
                Notificar("Não existe empresa com esse Id.");
                return false;
            }
            await _empresaRepository.Remover(id);
            return true;
        }
        public async Task<IEnumerable<Empresa>> Buscar(Expression<Func<Empresa, bool>> predicate)
        {
            return await _empresaRepository.Buscar(predicate);
        }
        public async Task<Empresa> ObterPorId(Guid id)
        {
            return await _empresaRepository.ObterPorId(id);
        }
        public async Task<Empresa> Obter(Guid id)
        {
            return await _empresaRepository.Obter(id);
        }
        public async Task<List<Empresa>> ObterTodos()
        {
            return await _empresaRepository.ObterTodos();
        }
        public async Task<Empresa> Buscar()
        {
            return await _empresaRepository.Buscar();
        }

        public void Dispose()
        {
            _empresaRepository?.Dispose();
        }
    }
}
