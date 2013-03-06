using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools {

	public class CollectionMessage {
		public string title { get; set; }
		public string code { get; set; }
		public IEnumerable<string> messages { get; set; }

		  public CollectionMessage( ) {
			messages = new List<string>( );
		}
	}

}