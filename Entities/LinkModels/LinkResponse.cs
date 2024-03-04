using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
	public class LinkResponse
	{
		public bool HasLinks { get; set; }
		public List<Entity> ShapedEntities { get; set; }
		public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }
		public LinkResponse()
		{
			ShapedEntities = new List<Entity>();
			LinkedEntities = new LinkCollectionWrapper<Entity>();
		}
	}
}
/*
LinkResponse Sınıfı:
HasLinks özelliği, yanıtın içinde bağlantıların olup olmadığını belirtir.
ShapedEntities özelliği, yanıt içinde dönülen ana veri varsa bu verilerin listesini tutar.
LinkedEntities özelliği, LinkCollectionWrapper<Entity> türünden bağlantıları ve bunlarla ilişkilendirilmiş varlık koleksiyonunu içerir.
Bu, yanıtın içinde dönülen ana verilerin yanında ilişkili bağlantıları da taşımak için kullanılabilir.
*/