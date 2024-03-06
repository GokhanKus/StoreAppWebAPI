using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api")]
	public class RootController : ControllerBase
	{
		private readonly LinkGenerator _linkGenerator;
		public RootController(LinkGenerator linkGenerator)
		{
			_linkGenerator = linkGenerator;
		}
		[HttpGet(Name = "GetRoot")]
		public async Task<IActionResult> GetRoot([FromHeader(Name = "Accept")] string mediaType)
		{
			if (mediaType.Contains("application/vnd.storeapp.apiroot"))
			{

			}
			return NoContent();
		}
	}
}
/*
Bu dökümantasyon, API'nin genel kullanımını, API'ye erişim yöntemlerini, belirli isteklerin ve yanıtların nasıl yapılandırılacağını ve 
genel olarak API'nin nasıl çalıştığını açıklar.
*/
