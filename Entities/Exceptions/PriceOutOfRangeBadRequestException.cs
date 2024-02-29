namespace Entities.Exceptions
{
	//PriceOutOfRangeBadRequest'u sealed oldugu kalıtım ile devralamazsin, yani bir bakima bu classin final version oldugunu soyluyoruz
	public sealed class PriceOutOfRangeBadRequestException : BadRequestException
	{
		public PriceOutOfRangeBadRequestException() : base("max price must be greater than min price")
		{

		}
	}
}
