using System;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using Machine.Specifications;
using Rhino.Mocks;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {
	
	[Subject(typeof(QueryConfiguration))]  
	public class QueryConfigurationSpecs{
		public abstract class concern : Observes<IQueryConfiguration, QueryConfiguration>{

			Establish c = () => {
				configuration = depends.on<IJsonCollectionConfiguration>();
				data_builder = depends.on<IDataBuilder>();
				filters =depends.on<object>();
			};

			protected static IJsonCollectionConfiguration configuration;
			protected static IDataBuilder data_builder;
			protected static object filters;
		}
   
		public class when_creating_a_query_with_sorting_options : concern{
			Establish c = () => {
				href = "blah";
				collection = new Collection { href = href };
				configuration.Stub(x => x.Collection).Return(collection);
				filter_data = fake.an<IEnumerable<Data>>();
				data_builder.Stub(x => x.GetDatasFor(filters.GetType(), filters)).Return(filter_data);

				sort_data= fake.an<IEnumerable<Data>>();
				data_builder.Stub(x => x.GetDatasFor(Arg<Type>.Is.Equal(typeof(TestSortModel)), Arg<object>.Is.Anything)).Return(sort_data);
			};

			Because b = () => result = sut.WithSortingFields<TestSortModel>();

			It should_add_the_filters_to_the_query_collection = () => {
				var filter = result.Collection.queries.First().filter;
				filter.href.ShouldEqual(href);
				filter.data.ShouldBeTheSameAs(filter_data);
			};

			It should_add_the_sorting_fiels_to_the_query_collection = () => {
				var filter = result.Collection.queries.First().sort;
				filter.href.ShouldEqual(href);
				filter.data.ShouldBeTheSameAs(sort_data);
			};

			static IJsonCollectionConfiguration result;
			static string href;
			static IEnumerable<Data> filter_data;
			static IEnumerable<Data> sort_data;
			static Collection collection;
		}
	}

	public class TestSortModel{}

}