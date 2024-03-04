namespace Entities.LinkModels
{
	public class LinkCollectionWrapper<T> : LinkResourceBase
	{
		public List<T> Value { get; set; } = new List<T>();

		public LinkCollectionWrapper(List<T> value)
		{
			Value = value;
		}
		public LinkCollectionWrapper()
		{

		}
	}
}
/*
LinkCollectionWrapper<T> Sınıfı:
LinkResourceBase sınıfını miras alıyor, böylece bu sınıfın içindeki Links özelliği, LinkResourceBase sınıfındaki gibi bağlantıları temsil ediyor.
Value özelliği, bu koleksiyonun içindeki nesnelerin listesini tutar.
LinkCollectionWrapper sınıfı, belirli bir kaynağın üzerinde bulunan bağlantıları ve bu kaynağın koleksiyonunu içeren bir yapı sağlar. 
Bu genellikle koleksiyonları alırken veya döndürürken bağlantıları da beraberinde göndermek için kullanılır.
*/