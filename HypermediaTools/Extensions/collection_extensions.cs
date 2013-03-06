using AvenidaSoftware.Extensions;
using AvenidaSoftware.HypermediaTools.Services;

namespace AvenidaSoftware.HypermediaTools.Extensions {
	
	public static class collection_extensions {

		public static  ICollectionJsonConfiguration add_link_if_name_is_not_null(this ICollectionJsonConfiguration json_configuration, Link link) {
			return link.name.is_not_null_nor_empty() ? json_configuration.AddLink(link) : json_configuration;
		}

	}

}