using Entities.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IDataShaper<T>
	{
		//ExpandoObject: memberlari runtimeda dinamik olarak eklenebilen ve kaldirilabilen bir obje saglar
		IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString);
		ShapedEntity ShapeData(T entities, string fieldsString);
	}
}
