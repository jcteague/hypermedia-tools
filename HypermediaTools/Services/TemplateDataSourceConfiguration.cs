using System;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class TemplateDataSourceConfiguration : ITemplateDataSourceConfiguration{
		public Type TemplateType { get; private set; }
		public ICollectionJsonConfiguration JsonConfiguration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }

		public TemplateDataSourceConfiguration(Type template_type, ICollectionJsonConfiguration json_configuration, IDataBuilder data_builder) {
			TemplateType = template_type;
			JsonConfiguration = json_configuration;
			DataBuilder = data_builder;
		}

		public ITemplateConfiguration<TDataSource> UseDataSource<TDataSource>(TDataSource data_source) {
			return new TemplateConfiguration<TDataSource>(TemplateType,JsonConfiguration, DataBuilder, data_source);
		}

		public CollectionJson Build() {
			return UseDataSource(new object()).Build();
		}
	}

}