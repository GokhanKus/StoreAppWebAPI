using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.RepoContracts
{
	//Unit of Work pattern
	public interface IRepositoryManager
	{
		IBookRepository BookRepository { get; }
		void Save();
	}
}
