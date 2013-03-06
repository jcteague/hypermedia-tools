using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools {

	public class FilterTemplate {
		public string href { get; set; }
		public IEnumerable<Data> data { get; set; }
	}

}