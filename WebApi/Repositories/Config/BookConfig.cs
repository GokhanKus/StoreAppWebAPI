using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace WebApi.Repositories.Config
{
	public class BookConfig : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasData(
				new Book { Id = 1, Price = 60.5m, Title = "Hacigoz ve Karivat", CreatedTime = DateTime.Now, },
				new Book { Id = 2, Price = 150, Title = "Tufek, Mikrop ve Celik", CreatedTime = DateTime.Now, },
				new Book { Id = 3, Price = 250, Title = "Devlet", CreatedTime = DateTime.Now, },
				new Book { Id = 4, Price = 45, Title = "Mesnevi", CreatedTime = DateTime.Now, }
				);
		}
	}
}
