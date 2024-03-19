using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	//Unit of Work pattern
	public interface IRepositoryManager
	{
		IBookRepository BookRepository { get; }
		ICategoryRepository CategoryRepository { get; }
		Task SaveAsync();
		//void Save(); void tipindekini async hale getirirken Task<void> yazmayiz direkt Task yazariz
	}
}
