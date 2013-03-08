using AvenidaSoftware.Objects;

namespace AvenidaSoftware.HypermediaTools {

	public class LinkType : Enumeration {
		public static readonly LinkType Normal = new LinkType { Value = "Normal" };
		public static readonly LinkType Action = new LinkType { Value = "Action" };
		public static readonly LinkType Bookmark = new LinkType { Value = "Bookmark" };
	}

}