using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.RepoContracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class CategoryService : ICategoryService
	{
		private readonly IRepositoryManager _manager;
		private readonly IMapper _mapper;
		public CategoryService(IRepositoryManager manager, IMapper mapper)
		{
			_manager = manager;
			_mapper = mapper;
		}

		public async Task CreateOneCategoryAsync(CategoryDtoForInsertion categoryDto)
		{
			//category'nin cok propertysi olmadigi icin mapleme islemi yapmadim, yine de yapilabilir
			var model = new Category { CategoryName = categoryDto.CategoryName, CreatedTime = DateTime.UtcNow };
			_manager.CategoryRepository.CreateOneCategory(model); 
			await _manager.SaveAsync();
		}

		public async Task DeleteOneCategoryAsync(int id, bool trackChanges)
		{
			var category = await GetOneCategoryById(id, trackChanges);
			_manager.CategoryRepository.DeleteOneCategory(category);
			await _manager.SaveAsync();
		}

		public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges)
		{
			var categories = await _manager.CategoryRepository.GetAllCategoriesAsync(trackChanges);
			return categories;
		}

		public async Task<Category> GetOneCategoryByIdAsync(int id, bool trackChanges)
		{
			var category = await GetOneCategoryById(id, trackChanges);
			return category;
		}

		public async Task UpdateOneCategoryAsync(int id, CategoryDtoForUpdate categoryDto, bool trackChanges)
		{
			var category = await GetOneCategoryById(id, trackChanges);
			_mapper.Map<CategoryDtoForUpdate>(category);
			await _manager.SaveAsync();
		}
		private async Task<Category> GetOneCategoryById(int id, bool trackChanges)
		{
			var category = await _manager.CategoryRepository.GetOneCategoryByIdAsync(id, trackChanges);
			if (category == null)
				throw new CategoryNotFoundException(id);
			return category;
		}
	}
}
