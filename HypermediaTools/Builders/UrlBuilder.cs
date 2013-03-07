using System;

namespace AvenidaSoftware.HypermediaTools.Builders {

	public class UrlBuilder {
		public static string CreateResourceUrl( string controller_name, string action, Guid entity_id ) {
			return "/api/" + controller_name.ToLower( ).Replace( "controller", "" ) + "/" + entity_id + "/" + action.ToLower( );
		}

		public static string CreateResourceUrl( string controller_name, string action ) {
			return "/api/" + controller_name.ToLower( ).Replace( "controller", "" ) + "/" + action.ToLower( );
		}

		public static string CreateResourceUrl< TController >( Guid entity_id ) {
			return CreateUrl( typeof( TController ) ) + "/" + entity_id;
		}

		public static string CreateResourceUrl< T >( Func<Guid, object> action, Guid entity_id ) {
			return CreateResourceUrl<T>( action.Method.Name, entity_id );
		}

		public static string CreateResourceUrl< TController >( Func<JsonCollection> action ) {
			return CreateUrl( typeof( TController ) ) + "/" + action.Method.Name;
		}

		public static string CreateResourceUrl< TController >( string action, Guid entity_id ) {
			return CreateUrl( typeof( TController ) ) + "/" + entity_id + "/" + action.ToLower( );
		}

		public static string CreateSharpedResourceUrl< TController >( ) {
			return CreateResourceUrl<TController>( ).Replace( "/api", "/#/api" );
		}

		public static string CreateResourceUrl<T>( ) {
			return CreateUrl( typeof(T) );
		}

		public static string CreateUrl( Type controller ) {
			return "/api/" + controller.Name.ToLower( ).Replace( "controller", "" ).ToLower( );
		}
	}

}