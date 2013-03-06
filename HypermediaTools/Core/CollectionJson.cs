using System.Collections.Generic;
using System.Net;

namespace AvenidaSoftware.HypermediaTools {

	public class CollectionJson {
		public Collection collection { get; set; }

		public static CollectionJson ForErrors( IEnumerable<string> errors, HttpStatusCode code, string title ) {
			var collection = new Collection {
				error = new CollectionMessage {
					code = code.ToString(),
					messages = errors,
					title = title ?? code.ToString( )
				}
			};

			return new CollectionJson { collection = collection };
		}

		public CollectionJson( ) {
			collection = new Collection();
		}
	}

}