using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoConcrete
{
	public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity, new()
	{
		protected readonly RepositoryContext _context;
		//burayi protected yaptik, cunku bu classı kalitim ile farklı repository.cslerde(classlarda) kullanırken buradaki contexti kullanmak isteriz
		//orn public BookRepository(RepositoryContext context) : base(context) yazarak buradaki contexti kullanmak isteriz
		protected RepositoryBase(RepositoryContext context)
		{
			_context = context;
		}
		public void Create(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
		}
		public void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
		}
		public void Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
		}
		public IQueryable<TEntity> FindAll(bool trackChanges)
		{
			return trackChanges ?
				_context.Set<TEntity>() :               //bir liste geldi ve ef core listeyi izleyecek
				_context.Set<TEntity>().AsNoTracking(); //degisiklikleri izlemeye gerek yok
		}
		public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges)
		{
			return trackChanges ?
				_context.Set<TEntity>().Where(expression) :
				_context.Set<TEntity>().Where(expression).AsNoTracking();
		}
	}
}