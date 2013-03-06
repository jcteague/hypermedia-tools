using System;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public interface ICollectionJsonBuilder {
		ICollectionJsonConfiguration Create( string href);
		ICollectionJsonConfiguration CreateFrom(CollectionJson collection);
		ICollectionJsonConfiguration Create<T>();
		ICollectionJsonConfiguration Create<T>(Guid id);
		ICollectionJsonConfiguration Create<T>( Func<CollectionJson> action );
	}

}