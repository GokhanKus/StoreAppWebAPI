using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
	public class DataShaper<T> : IDataShaper<T> where T : class
	{
		public PropertyInfo[] Properties { get; set; }
		public DataShaper()
		{
			Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}
		public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
		{
			var requiredProperties = GetRequiredProperties(fieldsString);
			return FetchDataForEntity(entities, requiredProperties);
		}

		public ExpandoObject ShapeData(T entity, string fieldsString)
		{
			var requiredProperties = GetRequiredProperties(fieldsString);
			return FetchDataForEntity(entity, requiredProperties);
		}
		private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldString)
		{
			var requiredFields = new List<PropertyInfo>();
			if (!string.IsNullOrWhiteSpace(fieldString))
			{
				var fields = fieldString.Split(',', StringSplitOptions.RemoveEmptyEntries); //books?field=id,,title,, gibi query girerse oncelikle alanlari ayiralim "id" "title"
				foreach (var field in fields)
				{
					PropertyInfo? property = Properties
						.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
					if (property is null)
						continue;
					requiredFields.Add(property);
				}
			}
			else
			{
				requiredFields = Properties.ToList();//eger client hicbir field sorgusu yazmadiysa butun alanlarin getirilmesini istiyor demektir o yuzden hepsini aldik.
			}
			return requiredFields;
		}
		private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
		{
			var shapedObject = new ExpandoObject();
			foreach (var property in requiredProperties)
			{
				var objectPropertyValue = property.GetValue(entity);
				shapedObject.TryAdd(property.Name, objectPropertyValue);
			}
			return shapedObject;
		}
		private IEnumerable<ExpandoObject> FetchDataForEntity(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
		{
			var shapedData = new List<ExpandoObject>();
			foreach (var entity in entities)
			{
				var shapedObject = FetchDataForEntity(entity, requiredProperties);
				shapedData.Add(shapedObject);
			}
			return shapedData;
		}
	}
}
