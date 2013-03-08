using System;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public interface IJsonCollectionBuilder {
		IJsonCollectionConfiguration Create( string href);
		IJsonCollectionConfiguration CreateFrom(JsonCollection json_collection);
		IJsonCollectionConfiguration Create<T>();
		IJsonCollectionConfiguration Create<T>(Guid id);
		IJsonCollectionConfiguration Create<T>( Func<JsonCollection> action );
	}

}