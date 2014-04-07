using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using AvenidaSoftware.HypermediaTools.Builders;

namespace AvenidaSoftware.HypermediaTools {
	public class Item {
		public Link self { get; set; }
		public IEnumerable<Data> data { get; set; }
		public IEnumerable<Link> links { get; set; }
		public CollectionMessage warnings { get; set; }
		public IEnumerable<EmbeddedCollection> embedded { get; set; }
		public	Item( ) {
			links = new List<Link>( );
			warnings = new CollectionMessage(  );
		}

		public static HttpResponseMessage For<T>( IEnumerable<Data> data, Guid entity_id, IList<string> warnings ) {
			var href = new Link { href = UrlBuilder.CreateResourceUrl<T>( entity_id ), prompt = "View Details" };
			var warnings_message = new CollectionMessage {title = "Warnings"};
			if( warnings.Count > 0 )	warnings_message.messages=warnings;
			var item = new Item { data = data, self = href, warnings = warnings_message};

			var http_content = new ObjectContent( item.GetType( ), item, new JsonMediaTypeFormatter( ) );
			var http_response_message = new HttpResponseMessage( HttpStatusCode.Created ) { Content = http_content };

			return http_response_message;
		}
	}

	public class EmbeddedCollection{
		public string name { get; set; }
		public IEnumerable<Item> items { get; set; }
	}

}