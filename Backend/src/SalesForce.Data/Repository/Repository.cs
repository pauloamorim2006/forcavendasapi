using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ERP.Business.Intefaces;
using ERP.Business.Models;
using ERP.Data.Context;
using Microsoft.EntityFrameworkCore;
using SalesForce.Business.Filter;
using SalesForce.Business.Responses;
using SalesForce.Data.Cache;
using System.Reflection;

namespace ERP.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly SalesForceDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly ICache Cache;

        protected Repository(SalesForceDbContext db, ICache cache)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
            Cache = cache;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> Obter(Guid id)
        {
            return await DbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<ResponseModel<TEntity>> ObterTodos(PaginationFilter filter)
        {
            var list = await Cache.GetListAsync<TEntity>(MethodBase.GetCurrentMethod().Name);
            if (list.Count() <= 0)
            {
                var data = await DbSet.
                    Skip((filter.PageNumber - 1) * filter.PageSize).
                    Take(filter.PageSize).
                    ToListAsync();
                var count = await DbSet.CountAsync();

                await Cache.SetListAsync<Entity>(MethodBase.GetCurrentMethod().Name, data);

                return new ResponseModel<TEntity>(data, count);
            }

            return new ResponseModel<TEntity>(list, list.Count());
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}