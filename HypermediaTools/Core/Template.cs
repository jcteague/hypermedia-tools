using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AvenidaSoftware.HypermediaTools {

	public class Template {
		[Required]
		public IEnumerable<Data> data { get; set; }
		
		public Template( ) {
			data = new List<Data>();
		}
	}

}