using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FizzWare.NBuilder;
using HypermediaTools.Attributes;
using HypermediaTools.CollectionBuilders;
using HypermediaTools.Models;
using HypermediaTools.Serialization;
using Machine.Specifications;
using Rhino.Mocks;
using StructureMap.Configuration.DSL;
using developwithpassion.specifications.rhinomocks;
using System.Linq;

namespace HyperMediaTools.UnitTests
{
    
    public class DataItemTester : IAmAResource, IRepresentAResource
    {
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        [RepresentationDataItem(Name="annotated",Prompt="Prompt")]
        public double DoubleProperty { get; set; }
        public DateTime DateProperty { get; set; }
        [EmbeddedResource]
        public IEnumerable<RelatedDataItem> LinkedRelatedItem { get; set; }

        public override string GetIdentifer()
        {
            throw new NotImplementedException();
        }

        
        public string GetResourceHref()
        {
            throw new NotImplementedException();
        }
    }
    public class RelatedDataItem : IAmAResource
    {
        public string RelatedField { get; set; }
        public override string GetIdentifer()
        {
            return "1";
        }

        public string GetResourceName()
        {
            return "RelatedItem";
        }
    }

    public class DataItemFormatterSpecs : Observes<DefaultDataItemFormatter<DataItemTester>>
    {
        Establish context = () =>
                                {
                                    field_serializer = depends.on<IFieldSerializer>();
                                    field_serializer.Stub(x => x.Serialize(1)).Return("value").IgnoreArguments();
                                    item_to_serialize = Builder<DataItemTester>.CreateNew()
                                        .With(x=>x.LinkedRelatedItem = (IEnumerable<RelatedDataItem>) Builder<RelatedDataItem>.CreateListOfSize(3).Build())
                                        .Build();
                                };

        Because of = () =>
                         {
                             result = sut.AsDataItem(item_to_serialize).ToList();
                             System.Console.WriteLine(result);
                         };

        It should_format_simple_field_types_and_embedded_types = () => result.Count.ShouldEqual(4);
        It should_set_the_value_from_the_serializer = () =>
                                                          {
                                                              result.Any(x => x.name == "IntProperty").ShouldBeTrue();
                                                              result.Any(x => x.name == "StringProperty").ShouldBeTrue();
                                                              result.Any(x => x.name == "DateProperty").ShouldBeTrue();
                                                              result.All(x => x.value == "value").ShouldBeTrue();
                                                          };
        
        
        protected static DataItemTester item_to_serialize;
        protected static List<dynamic> result;
        static IFieldSerializer field_serializer;
    }


    public class when_the_property_is_not_decorated_with_the_data_item_attribute : DataItemFormatterSpecs
    {
        It should_use_the_property_name_for_the_name = () => result[0].name.ShouldEqual("IntProperty");
        It should_use_the_property_name_for_the_prompt = () => result[0].prompt.ShouldEqual("IntProperty");
    }

    public class and_is_decorated_with_the_data_attribute : DataItemFormatterSpecs
    {
        It should_use_the_name_annotation_for_the_name = () => result[2].name.ShouldEqual("annotated");
        It should_use_the_prompt_annotation_for_the_prompt = () => result[2].prompt.ShouldEqual("Prompt");
    }
    public class when_formatting_an_embedded_resource : DataItemFormatterSpecs
    {
        It should_be_an_array_of_data_items = () =>
                                                  {
                                                      result[4].Count.ShouldEqual(1);
                                                  };
    }
}