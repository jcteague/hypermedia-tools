using System.Collections.Generic;
using HypermediaTools.Models;

namespace HypermediaTools.CollectionBuilders
{
    public interface IBuildCollectionItems<Resource> where Resource: IAmAResource
    {
        CollectionItem GetCollectionItems(Resource representation, bool allowEmbedded = true);
    }
    public class CollectionItemBuilder<Resource> : IBuildCollectionItems<Resource> where Resource : IAmAResource 
    {
        IFormatAsDataItem<Resource> data_item_formatter;
        IBuildLinks<Resource> link_builder;
        ITemplateBuilder<Resource> template_builder;
        public CollectionItemBuilder(IFormatAsDataItem<Resource> dataItemFormatter, IBuildLinks<Resource> linkBuilder, ITemplateBuilder<Resource> templateBuilder)
        {
            data_item_formatter = dataItemFormatter;
            link_builder = linkBuilder;
            template_builder = templateBuilder;
        }
        public CollectionItem GetCollectionItems(Resource resource, bool allowEmbedded = true)
        {

            return new CollectionItem()
                       {
                           Href = resource.GetResourceHref(),
                           Data = data_item_formatter.AsDataItem(resource,allowEmbedded),
                           Links = link_builder.GetLinks(resource)
                       };
        }
    }
}