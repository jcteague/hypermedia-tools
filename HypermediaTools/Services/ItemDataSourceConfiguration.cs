using System;
using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {

	public class ItemDataSourceConfiguration : IItemDataSourceConfiguration {
		public Type TemplateType { get; set; }
		public ICollectionJsonConfiguration CollectionJsonConfiguration { get; set; }
		public IDataBuilder DataBuilder { get; set; }

		public ItemDataSourceConfiguration( Type template_type, ICollectionJsonConfiguration collection_json_configuration, IDataBuilder data_builder ) {
			TemplateType = template_type;
			CollectionJsonConfiguration = collection_json_configuration;
			DataBuilder = data_builder;
		}
		
		public IItemConfiguration<TDataSource> UseDataSource<TDataSource>(TDataSource data_source){
			var data_sources = new List<TDataSource> { data_source };
			return UseDataSource<TDataSource>(data_sources);
		}

		public IItemConfiguration<TDataSource> UseDataSource<TDataSource>( IEnumerable<TDataSource> data_sources ){
			return new ItemConfiguration<TDataSource>(data_sources, TemplateType,CollectionJsonConfiguration,DataBuilder);
		}
	}

}