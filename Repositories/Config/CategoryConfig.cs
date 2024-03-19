using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Config
{
	public class CategoryConfig : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			//builder.HasKey(c => c.Id); eger category classinin Id veya CategoryId adinda propu varsa otomatik olarak pk(primary key olur), ancak farklı isimde ise bu sekilde pk yapabiliriz.
			builder.Property(c => c.CategoryName).IsRequired();//category adı zorunlu
			builder.HasData(
				new Category { Id = 1, CategoryName = "Psychology Thriller", CreatedTime = DateTime.Now },
				new Category { Id = 2, CategoryName = "Adventure", CreatedTime = DateTime.Now },
				new Category { Id = 3, CategoryName = "History", CreatedTime = DateTime.Now }
				);
		}
	}
}
