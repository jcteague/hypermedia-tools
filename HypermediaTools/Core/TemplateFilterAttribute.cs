using System;

namespace AvenidaSoftware.HypermediaTools {

	public class TemplateFilterAttribute : Attribute {
		public string Prompt { get; set; }

		public string Name { get; set; }

		public string Value { get; set; }

		public string Type { get; set; }

		public string Group { get; set; }
	}

}