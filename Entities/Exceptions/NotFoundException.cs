using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	public abstract class NotFoundException : Exception //NotFound abstract oldugu icin newlenemez
	{
		//eger NotFound classi kalitim ile alinacaksa o classta ctor icinde message parametresi verilmek zorunda
		//ctor(string message) : base(message)
		protected NotFoundException(string message) : base(message)
		{

		}
	}
	
}
