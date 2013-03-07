using System.Collections.Generic;
using System.Net;

namespace AvenidaSoftware.HypermediaTools {

	public class JsonCollection {
		public Collection collection { get; set; }

		public static JsonCollection ForErrors( IEnumerable<string> errors, HttpStatusCode code, string title ) {
			var collection = new Collection {
				error = new CollectionMessage {
					code = code.ToString(),
					messages = errors,
					title = title ?? code.ToString( )
				}
			};

			return new JsonCollection { collection = collection };
		}

		public JsonCollection( ) {
			collection = new Collection();
		}
	}

}