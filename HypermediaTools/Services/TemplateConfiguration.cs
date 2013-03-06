﻿using System;
using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class TemplateConfiguration<TDataSource> : ITemplateConfiguration<TDataSource> {
		public Type TemplateType { get; private set; }
		public ICollectionJsonConfiguration JsonConfiguration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }
		public TDataSource DataSource { get; private set; }
		public List<Func<TDataSource,Data>> CustomsDataBuilder { get; private set; }

		public TemplateConfiguration(Type template_type, ICollectionJsonConfiguration json_configuration, IDataBuilder data_builder, TDataSource data_source) {
			TemplateType = template_type;
			JsonConfiguration = json_configuration;
			DataBuilder = data_builder;
			DataSource = data_source;
			CustomsDataBuilder =new List<Func<TDataSource, Data>>();
		}

		public ICollectionJsonConfiguration AddLink(Link link) {
			return JsonConfiguration.AddLink(link);
		}

		public void SetUrl(string url) {
			JsonConfiguration.SetUrl(url);
		}

		public Collection Collection {
			get { return JsonConfiguration.Collection; }
		}

		public IItemDataSourceConfiguration AddItemsFor<ItemModel>(){
			Build();
			return JsonConfiguration.AddItemsFor<ItemModel>();
		}

		public ITemplateDataSourceConfiguration AddTemplateFor<TTemplate>() {
			Build();
			return JsonConfiguration.AddTemplateFor<TTemplate>();
		}

		public IQueryConfiguration AddQueryFor(object filter) {
			Build();
		   return JsonConfiguration.AddQueryFor(filter);
		}
		

		// ADD CUSTOM DATA
		public ITemplateConfiguration<TDataSource> AddCustomData( Func<TDataSource, Data> data_template_builder) {
			CustomsDataBuilder.Add( data_template_builder );
			return this;
		}

		public ITemplateConfiguration<TDataSource> AddCustomData( Func<TDataSource, IEnumerable<Data>> data_template_builder ) {
			foreach( var data in data_template_builder(DataSource) ) {
				var new_data = new Data{
					value = data.value,
					name = data.name,
					type = data.type,
					prompt = data.prompt,
					acceptable_values = data.acceptable_values
				};

				AddCustomData(x => new_data);
			}

			return this;
		}
		
		public ITemplateConfiguration<TDataSource> AddCustomData( Data data ) {
			AddCustomData( x=> data );
			return this;
		}

		public CollectionJson Build() {
			var temp_data = new List<Data>();

			if( Collection.template.data != null ) {
				temp_data.AddRange( Collection.template.data ); 
			}

			var data = DataBuilder.GetDatasFor( TemplateType, DataSource );
			temp_data.AddRange(data);

			foreach (var custom_data_builder in CustomsDataBuilder) {
				temp_data.Add(custom_data_builder(DataSource));
			}

			Collection.template  = new Template{data = temp_data};

			return JsonConfiguration.Build();
		}
	}

}