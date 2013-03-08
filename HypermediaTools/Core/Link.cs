namespace AvenidaSoftware.HypermediaTools {

	public class Link {
		public string href { get; set; }
		public string rel { get; set; }
		public string name { get; set; }
		public string render { get; set; }
		public string prompt { get; set; }
		public LinkType type { get; set; }
		public string method { get; set ; }
	}

}