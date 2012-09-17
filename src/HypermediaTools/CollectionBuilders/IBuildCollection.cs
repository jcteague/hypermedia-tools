using System;
using System.Collections.Generic;
using HypermediaTools.Models;
using HypermediaTools.CollectionBuilders;

namespace HypermediaTools.CollectionBuilders
{
    public interface IBuildCollection<Resource> where Resource: IAmAResource
    {
        CollectionModel BuildCollection(string apiUriFragment, IEnumerable<Resource> resources);
    }


    public class CollectionBuilder<Resource> : IBuildCollection<Resource> 
        where Resource : IAmAResource
    {
        IBuildCollectionItems<Resource> collection_item_builder;
        ITemplateBuilder<Resource> template_builder; 
        public CollectionBuilder(IBuildCollectionItems<Resource> collectionItemBuilder, ITemplateBuilder<Resource> templateBuilder)
        {
            collection_item_builder = collectionItemBuilder;
            template_builder = templateBuilder;
        }

        public CollectionModel BuildCollection(string apiUriFragment, IEnumerable<Resource> resources)
        {
            var model = new CollectionModel();
            foreach (var resource in resources)
            {
                model.AddCollectionItem(collection_item_builder.GetCollectionItems(resource,false));
            }
            model.template = template_builder.BuildTemplate();
            var resourceName = typeof (Resource).GetResourceName();
            model.self = new Link
                             {
                                 href = apiUriFragment + "/" + resourceName,
                                 rel = resourceName
                             };
            //model.AddCollectionLink(new Link(){href = model.self,name = "create",prompt = "Create",rel = "Create"});

            return model;
            
        }
    }


    
}