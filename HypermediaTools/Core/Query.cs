using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools {

	public class Query {
		public QueryOption filter { get; set; }
		public QueryOption sort{ get; set; }

		public Query() {
			filter = new QueryOption();
			sort = new QueryOption();
		}

	}

	public class QueryOption{
		 public string href { get; set; }
		public string rel { get; set; }
		public string name { get; set; }
		public string prompt { get; set; }
		public IEnumerable<Data> data { get; set; }
	}

}