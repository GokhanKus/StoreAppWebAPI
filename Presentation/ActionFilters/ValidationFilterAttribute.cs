using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
	//bu class sayesinde artik controllerlarda null kontrolu veya modelstate.Isvalid kontrolu yapmayacagiz cunku artık burada yapiyoruz
	//artik tek yapmamız gereken sey controllerdaki action metotlarının uzerine [ValidationFilter] yazmak
	internal class ValidationFilterAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context) //controllerdaki action metodu calismadan hemen once calisacak olan metot burasi
		{
			var controller = context.RouteData.Values["controller"]; //urldeki controller ismini verir 
			var action = context.RouteData.Values["action"];        //urldeki action ismini verir

			var parameter = context.ActionArguments.SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value; //actionda dto keywordu gecen bir parametre varsa onu alır
			if (parameter is null)	//eger dto yoksa
			{
				context.Result = new BadRequestObjectResult($"object is null in Controller: {controller} Action: {action}");
				return; //400
			}
			if (!context.ModelState.IsValid)
			{
				context.Result = new UnprocessableEntityObjectResult(context.ModelState); 
				return;
			}
		}
	}
}

/*
ActionFilter bir controller ya da controller icerisindeki action yapisina uygulanan ve bu yolla ilgili yapinin duzenlenmesine olanak saglayan bir attribute'dur.
[ActionFilter] attributesi ilgili action calismadan once ya da calistiktan sonra bazı kodlari modifiye etmemize olanak saglar.
Log alma, cache islemleri, try catch, authorization, authentication, exception hata yonetimi vs icin bu yapi kullanilabilir.
Autherization filters, resource filters, action filters, exception filters, result filters, 
ActionFilterlarda => IActionFilter, IAsyncFilter, ActionFilterAttribute 
ve bunların kaydini Global(addcontroller icinde kayit edilir), controller level ve action level'da(services seviyesinde IOC kaydi edilir) yapabiliriz.
 */
