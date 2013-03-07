using System;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class TemplateDataSourceConfiguration : ITemplateDataSourceConfiguration{
		public Type TemplateType { get; private set; }
		public IJsonCollectionConfiguration Configuration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }

		public TemplateDataSourceConfiguration(Type template_type, IJsonCollectionConfiguration configuration, IDataBuilder data_builder) {
			TemplateType = template_type;
			Configuration = configuration;
			DataBuilder = data_builder;
		}

		public ITemplateConfiguration<TDataSource> UseDataSource<TDataSource>(TDataSource data_source) {
			return new TemplateConfiguration<TDataSource>(TemplateType,Configuration, DataBuilder, data_source);
		}

		public JsonCollection Build() {
			return UseDataSource(new object()).Build();
		}
	}

}