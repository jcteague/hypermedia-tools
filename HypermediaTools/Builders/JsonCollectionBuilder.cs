using System;
using AvenidaSoftware.HypermediaTools.Services;

namespace AvenidaSoftware.HypermediaTools.Builders {
	
	public class JsonCollectionBuilder : IJsonCollectionBuilder{
		readonly IJsonCollectionConfiguration json_collection_configuration;

		public JsonCollectionBuilder(IJsonCollectionConfiguration json_collection_configuration) {
			this.json_collection_configuration = json_collection_configuration;
		}

		public IJsonCollectionConfiguration Create(string href) {
			json_collection_configuration.SetUrl(href);
			return json_collection_configuration;
		}

		public IJsonCollectionConfiguration Create<T>() {
			return Create( UrlBuilder.CreateResourceUrl<T>() );
		}

		public IJsonCollectionConfiguration Create<T>(Guid id) {
			return Create( UrlBuilder.CreateResourceUrl<T>(id) );
		}

		public IJsonCollectionConfiguration Create<T>( Func<JsonCollection> action ) {
			return Create( UrlBuilder.CreateResourceUrl<T>(action) );
		}

		public IJsonCollectionConfiguration CreateFrom(JsonCollection json_collection) {
			json_collection_configuration.Collection.href = json_collection.collection.href;
			json_collection_configuration.Collection.items = json_collection.collection.items;
			json_collection_configuration.Collection.links = json_collection.collection.links;
			json_collection_configuration.Collection.template= json_collection.collection.template;
			json_collection_configuration.Collection.queries= json_collection.collection.queries;
			json_collection_configuration.Collection.error= json_collection.collection.error;

			return json_collection_configuration;
		}
	}

}