using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ERP.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository,
                                 INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Adicionar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return false;

            if (_clienteRepository.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
            {
                Notificar("Já existe um cliente com este documento informado.");
                return false;
            }

            await _clienteRepository.Adicionar(cliente);
            return true;
        }
        public async Task<bool> Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return false;

            if (_clienteRepository.JaExisteCliente(cliente.Id, cliente.CnpjCpfDi))
            {
                Notificar("Já existe um cliente com este documento informado.");
                return false;
            }

            await _clienteRepository.Atualizar(cliente);
            return true;
        }
        public async Task<bool> Remover(Guid id)
        {
            if (!_clienteRepository.EncontrouCliente(id))
            {
                Notificar("Já existe um cliente com este documento informado.");
                return false;
            }
            try
            {
                await _clienteRepository.Remover(id);
                return true;
            } 
            catch
            {
                Notificar("Cliente não pode ser excluído pois já existem relacionamentos com ele.");
                return false;
            }
            
        }
        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            return await _clienteRepository.Buscar(predicate);
        }
        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await _clienteRepository.ObterPorId(id);
        }
        public async Task<Cliente> Obter(Guid id)
        {
            return await _clienteRepository.Obter(id);
        }
        public async Task<List<Cliente>> ObterTodos()
        {
            return await _clienteRepository.ObterTodos();
        }
        public async Task<int> RecuperarQuantidade()
        {
            return await _clienteRepository.RecuperarQuantidade();
        }        
        public async Task<List<Cliente>> RecuperarTodos()
        {
            return await _clienteRepository.RecuperarTodos();
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
        }
    }
}
