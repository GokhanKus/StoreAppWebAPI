using WebApi.Utilities.Formatters;

namespace WebApi.ExtensionMethods
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddCustomCsvFormatter(this IMvcBuilder builder)
		{
			return builder.AddMvcOptions(config =>
			{
				config.OutputFormatters.Add(new CsvOutputFormatter());
			});
		}
	}
}
