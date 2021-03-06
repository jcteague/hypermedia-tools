﻿using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class JsonCollectionConfiguration : IJsonCollectionConfiguration{
		readonly IDataBuilder data_builder;

		public Collection Collection { get; set; }

		public JsonCollectionConfiguration(IDataBuilder data_builder) {
			this.data_builder = data_builder;
			Collection = new Collection();
		}

		public void SetUrl(string url) {
			Collection.href = url;
		}

		public JsonCollection Build() {
			return new JsonCollection{ collection = Collection };
		}
		
		public IJsonCollectionConfiguration AddLink(Link link) {
			var links = new List<Link>(Collection.links);
			
			links.Add(link);
			Collection.links = links;

			return this;
		}

		public IItemDataSourceConfiguration AddItemsFor<ItemModel>(){
			return new ItemDataSourceConfiguration( typeof(ItemModel), this, data_builder );
		}

		public ITemplateDataSourceConfiguration AddTemplateFor<TTemplateModel>() {
			return new TemplateDataSourceConfiguration( typeof(TTemplateModel), this, data_builder );
		}

		public IQueryConfiguration AddQueryFor(object filter) {
			return new QueryConfiguration( this, data_builder, filter );
		}
	}

}