using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	public interface IRepositoryBase<TEntity>
	{
		IQueryable<TEntity> FindAll(bool trackChanges);
		TEntity? FindByCondition(Expression <Func<TEntity, bool>> expression, bool trackChanges); //burası sorgu sonucu bir varlık koleksiyonu döndürür, tek bir entity dondurmek istiyorsak IQueryable silersin
		void Create(TEntity entity);
		void Update(TEntity entity); 
		void Delete(TEntity entity); 
	}
}
//ef coreda datayı ve degisiklikleri izleme ozelligi vardir(track changes) degisikleri izler ve SaveChanges() dedigimizde degisiklikleri yansitir
//bazen degisiklikleri izlememek bize performans kazandirir her zaman izlemeye gerek olmaz
