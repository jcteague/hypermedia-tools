using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools {

	public class SortingTemplate {
		public string href { get; set; }
		public IEnumerable<Data> data { get; set; }
	}

}