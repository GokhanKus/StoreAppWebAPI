using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
	//BadRequest abstract oldugu icin newlenemez (newlenemedigi icin ctorun public olmasinin anlami yok, cunku ctor o class newlendigi zaman calisir ama abstract oldugu icin newlenemiyor)
	public abstract class BadRequestException : Exception
	{
		//eger BadRequest classi kalitim ile alinacaksa o classta ctor icinde message parametresi verilmek zorunda
		//ctor(string message) : base(message)
		protected BadRequestException(string message) : base(message)
		{

		}
	}
}
