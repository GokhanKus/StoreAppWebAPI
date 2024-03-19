using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	public interface ICategoryRepository : IRepositoryBase<Category>
	{
		Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
		Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges);
		void CreateOneCategory(Category category);
		void UpdateOneCategory(Category category);
		void DeleteOneCategory(Category category);
	}
}
/*
Genellikle IQueryable<T> döndüren bir metot, veri tabanından veri çekerken daha fazla esneklik sağlar ve sorgu sonuçlarını daha fazla işlemek için kullanılabilir.
Bu nedenle, büyük veri setlerini işlerken veya sorgular üzerinde dinamik filtrelemeler yaparken tercih edilir.

Eğer ORM aracınız IQueryable<T> üzerinde çalışıyorsa ve veri tabanından veri çekme işlemlerinizde filtreleme, sıralama gibi dinamik operasyonlar yapmanız gerekiyorsa,
Task<IQueryable<Category>> GetAllCategories(bool trackChanges); şeklindeki metodu tercih etmelisiniz.

Ancak, eğer sorgularınız genellikle basit ve önceden belirlenmiş ise ve veri tabanından veri çekme işlemlerinizde ekstra sorgu işlemleri yapmayacaksanız,
IEnumerable<T> döndüren Task<IEnumerable<Category>> GetAllCategories(bool trackChanges); metodu daha uygundur.
*/