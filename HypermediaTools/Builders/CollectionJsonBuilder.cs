using System;
using AvenidaSoftware.HypermediaTools.Services;

namespace AvenidaSoftware.HypermediaTools.Builders {
	
	public class CollectionJsonBuilder : ICollectionJsonBuilder{
		readonly ICollectionJsonConfiguration collection_json_configuration;

		public CollectionJsonBuilder(ICollectionJsonConfiguration collection_json_configuration) {
			this.collection_json_configuration = collection_json_configuration;
		}

		public ICollectionJsonConfiguration Create(string href) {
			collection_json_configuration.SetUrl(href);
			return collection_json_configuration;
		}

		public ICollectionJsonConfiguration Create<T>() {
			return Create( UrlBuilder.CreateResourceUrl<T>() );
		}

		public ICollectionJsonConfiguration Create<T>(Guid id) {
			return Create( UrlBuilder.CreateResourceUrl<T>(id) );
		}

		public ICollectionJsonConfiguration Create<T>( Func<CollectionJson> action ) {
			return Create( UrlBuilder.CreateResourceUrl<T>(action) );
		}

		public ICollectionJsonConfiguration CreateFrom(CollectionJson collection) {
			collection_json_configuration.Collection.href = collection.collection.href;
			collection_json_configuration.Collection.items = collection.collection.items;
			collection_json_configuration.Collection.links = collection.collection.links;
			collection_json_configuration.Collection.template= collection.collection.template;
			collection_json_configuration.Collection.queries= collection.collection.queries;
			collection_json_configuration.Collection.error= collection.collection.error;

			return collection_json_configuration;
		}
	}

}