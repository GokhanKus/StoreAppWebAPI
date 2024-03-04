using Entities.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
	public class ValidateMediaTypeAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context) //actiondan (metot) once calisacak
		{
			var acceptHeaderPresent = context.HttpContext
				.Request.Headers.ContainsKey("Accept");

			if (!acceptHeaderPresent) //request'in header'ında "accept" ifadesi yoksa;
			{
				context.Result = new BadRequestObjectResult("accept header is missing!");
				return;
			}
			var mediaType = context.HttpContext.Response.Headers["Accept"].FirstOrDefault();
			if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))//accept basligi varsa, ancak media turu gecerli degilse;
			{
				context.Result = new BadRequestObjectResult($"Media type not present. " +
					$"Please add Accept header with required media type.");
				return;
			}

			context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);

			//"Accept" başlığının değerini analiz eder ve sonucu out parametresi üzerinden outMediaType adlı değişkene atar.
			//Bu, MediaTypeHeaderValue.TryParse metodunun döndüreceği başarılı veya başarısız işlemin sonucunu outMediaType değişkeninde depolar.
			//Eğer işlem başarısız olursa, outMediaType null olacaktır.
		}
	}
}
