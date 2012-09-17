using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using HypermediaTools.CollectionBuilders;
using HypermediaTools.Models;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;
using System.Linq;
using Rhino.Mocks;

namespace HyperMediaTools.UnitTests
{
    public class ResourceA : IAmAResource, IRepresentAResource
    {
        public string Name { get; set; }
        public RelatedResource Related { get; set; }
        public override string GetIdentifer()
        {
            return "x";
        }

        public string GetResourceName()
        {
            return "ResourceA";
        }

        public string GetResourceHref()
        {
            return "/ResourceA/x";
        }
    }
    public class RelatedResource : IAmAResource
    {
        public override string GetIdentifer()
        {
            return "y";
        }

        public string GetResourceName()
        {
            return "Related";
        }
    }

    public class CollectionItemBuilderSpecs : Observes<CollectionItemBuilder<ResourceA>>
    {
        Establish context = () =>
                                {
                                    the_resource = Builder<ResourceA>.CreateNew().Build();
                                    data_items = Builder<Data>.CreateListOfSize(1).Build();
                                    links = Builder<Link>.CreateListOfSize(1).Build();
                                     data_item_formatter = depends.on<IFormatAsDataItem<ResourceA>>();
                                    data_item_formatter.Stub(x => x.AsDataItem(the_resource)).Return(data_items);
                                    link_builder = depends.on<IBuildLinks<ResourceA>>();
                                    link_builder.Stub(x => x.GetLinks(the_resource)).Return(links);
                                };

        Because of = () => result = sut.GetCollectionItems(the_resource);

        It should_set_the_href_value_to_name_of_class = () => result.Href.ShouldEqual("/ResourceA/x");
        It should_return_the_array_of_data_items = () => result.Data.ShouldEqual(data_items);

        It should_retun_the_list_related_resources = () => result.Links.ShouldEqual(links);

        static ResourceA the_resource;
        static CollectionItem result;
        static IFormatAsDataItem<ResourceA> data_item_formatter;
        static IList<Data> data_items;
        static IBuildLinks<ResourceA> link_builder;
        static IList<Link> links;
    }
}