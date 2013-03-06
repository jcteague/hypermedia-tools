using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools {
	
	public class Collection {
		public string version {
			// TODO: consider returning the assembly version
			get { return "1.0"; }
		}

		public string href { get; set; }

		public IEnumerable<Link> links { get; set; }

		public IEnumerable<Item> items { get; set; }

		public IEnumerable<Query> queries { get; set; }

		public Template template { get; set; }

		public CollectionMessage error { get; set; }

		public CollectionMessage warnings { get; set; }

		public Collection( ) {
			links = new List<Link>( );
			template = new Template( );
			items = new List<Item>();
			error = new CollectionMessage(  );
			warnings = new CollectionMessage(  );
		}
	}

}