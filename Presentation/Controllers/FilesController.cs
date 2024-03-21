using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.Net.WebRequestMethods;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/files")]
	public class FilesController : ControllerBase
	{
		[HttpPost("upload")]
		public async Task<IActionResult> Upload(IFormFile file) // => Upload/Post
		{
			if (!ModelState.IsValid)
				return BadRequest();

			//folder
			var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media"); //=>  "C:\\ASP.NET_Projects\\StoreAppWebAPI\\WebApi\\Media"
			if (!Directory.Exists(folder))          //yoksa olusturalim
				Directory.CreateDirectory(folder);
			//path
			var path = Path.Combine(folder, file.FileName);
			//stream
			using (var stream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			//response body
			return Ok(new
			{
				file = file.FileName,
				path = path,
				size = file.Length
			});
		}
		[HttpGet("download")]
		public async Task<IActionResult> Download(string fileName) // => Download/Get
		{
			//filePath
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Media", fileName);

			//ContentType : (MIME)
			//HTTP isteğinin yanıtında içerik türünü belirlemek için kullanılan bir FileExtensionContentTypeProvider örneği oluşturulur.
			var provider = new FileExtensionContentTypeProvider();

			//ContentType : (MIME)
			//Belirtilen dosya adı için içerik türünü (MIME türü) almak için FileExtensionContentTypeProvider tarafından TryGetContentType() metodu kullanılır.
			//Eğer dosya adına uygun bir içerik türü bulunamazsa, varsayılan olarak "application/octet-stream" kullanılır.
			if (!provider.TryGetContentType(fileName, out var contentType))
			{
				contentType = "application/octet-stream";
			}

			//Read
			//Dosyanın içeriğini okumak için System.IO.File sınıfının ReadAllBytesAsync() metodu kullanılır.
			//Bu, belirtilen dosyanın tamamını asenkron olarak bir byte dizisi olarak okur. await ifadesi, işlem dosya okuma tamamlanana kadar bekletilir.
			var bytes = await System.IO.File.ReadAllBytesAsync(filePath);

			// Metot, dosyanın içeriğini HTTP yanıtı olarak döndürür. File() metodu, bir byte dizisi, dosyanın içerik türü ve dosya adı gibi parametreleri alır.
			// Bu, dosyanın içeriğini istemciye aktarırken, içerik türünü ve dosya adını belirtir.
			return File(bytes, contentType, Path.GetFileName(filePath));
		}
	}
}
