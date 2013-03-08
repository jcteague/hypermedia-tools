using AvenidaSoftware.Extensions;
using AvenidaSoftware.HypermediaTools.Services;

namespace AvenidaSoftware.HypermediaTools.Extensions {
	
	public static class collection_extensions {

		public static  IJsonCollectionConfiguration add_link_if_name_is_not_null(this IJsonCollectionConfiguration configuration, Link link) {
			return link.name.is_not_null_nor_empty() ? configuration.AddLink(link) : configuration;
		}

	}

}