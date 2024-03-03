namespace Entities.RequestFeatures
{
	public class BookParameters : RequestParameters
	{
		public uint MinPrice { get; set; }
		public uint MaxPrice { get; set; } = 1000;
		public bool IsValid => MaxPrice >= MinPrice;
        public BookParameters()
        {
			OrderBy = "id"; //client orderby ifadesi girmezse default olarak OrderBy id olsun
        }
    }
}