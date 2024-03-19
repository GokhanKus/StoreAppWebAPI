using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.RepoContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoConcrete
{
	public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
	{
		public CategoryRepository(RepositoryContext context) : base(context)
		{
		}
		public void CreateOneCategory(Category category) => Create(category);
		public void UpdateOneCategory(Category category) => Update(category);
		public void DeleteOneCategory(Category category) => Delete(category);
		public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
		{
			return await FindAll(trackChanges).OrderBy(c => c.CategoryName).ToListAsync();
		}
		public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
		{
			return await FindByCondition((c => c.Id.Equals(id)), trackChanges).FirstOrDefaultAsync();

		}
	}
}
/*
Equals: Bu metot, nesnelerin derinlemesine karşılaştırılmasını sağlar. İki nesnenin değerlerinin aynı olup olmadığını kontrol eder. 
Genellikle referans türlerinde kullanılır ve nesnelerin içeriğinin karşılaştırılmasını sağlar. Ancak, Equals metodu override edilmemişse,
varsayılan olarak referans karşılaştırması yapar.

== (eşitlik operatörü): Bu operatör, iki değerin eşit olup olmadığını kontrol eder. Değer türleri için değerlerin eşitliğini kontrol eder,
referans türleri için ise bellek adreslerini karşılaştırır.

Örneğin, string türünde Equals metodu, iki stringin içeriğini karşılaştırırken, == operatörü, iki stringin bellek adresini karşılaştırır.
Ancak, string türünde == operatörü, içerik karşılaştırması yapacak şekilde özel olarak aşırtılmıştır.

Bu bağlamda, sizin verdiğiniz örnekte, Equals ve == aynı sonucu verebilir, çünkü Id bir değer türü olabilir ve referans karşılaştırması yapmak yerine
değer karşılaştırması yapılmak isteniyor olabilir. Bu durumda, hangi operatörü kullanırsanız kullanın, sonuç aynı olacaktır.
*/
