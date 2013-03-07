using System;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using FizzWare.NBuilder.Generators;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {
	
	[ Subject( typeof( TemplateConfiguration<TestData> ) ) ]
	public class TemplateConfigurationSpecs {
		public abstract class concern : Observes<ITemplateConfiguration<TestData>, TemplateConfiguration<TestData>> {
			Establish c = ( ) => {
				template_type = depends.on( typeof( object ) );
				json_collection_configuration = depends.on<IJsonCollectionConfiguration>( );
				DataBuilder = depends.on<IDataBuilder>( );
			};

			protected static Type template_type;
			protected static IJsonCollectionConfiguration json_collection_configuration;
			protected static IDataBuilder DataBuilder;
		}

		public class building_concern : concern {
			Establish c = ( ) => {
				test_data_source = new TestData { Name = GetRandom.String( 10 ) };

				depends.on( test_data_source );

				url = GetRandom.String( 10 );
				other_data = new Data( );
				collection = new Collection { href = url, items = new List<Item>( ), template = new Template { data = new List<Data> { other_data } } };

				json_collection_configuration.Stub( x => x.Collection ).Return( collection );
				datas = new List<Data> { test_data };

				DataBuilder.Stub( x => x.GetDatasFor( template_type, test_data_source ) ).Return( datas );

				json_collection_configuration.Stub( x => x.Build( ) ).Return( json_collection );
				custom_data = new Data( );
			};

			protected static TestData test_data_source;
			protected static Collection collection;
			protected static string url;
			protected static IEnumerable<Data> datas;
			protected static JsonCollection result;
			protected static JsonCollection json_collection;
			protected static Data test_data;
			protected static Data other_data;
			protected static Data custom_data;
		}

		public class when_building_a_template_with_custom_data : building_concern {
			Because b = ( ) => {
				sut.AddCustomData( custom_data );
				result = sut.Build( );
			};

			It should_return_the_correct_items = ( ) => {
				var item = collection.template;
				item.data.ElementAt( 0 ).ShouldBeTheSameAs( other_data );
				item.data.ElementAt( 1 ).ShouldBeTheSameAs( test_data );
				item.data.ElementAt( 2 ).ShouldBeTheSameAs( custom_data );
			};

			It should_return_the_collection_json_of_json_configuration = ( ) => result.ShouldBeTheSameAs( json_collection );
		}

		public class when_adding_new_link_to_the_collection : building_concern {
			Establish c = ( ) => {
				link = new Link( );
				json_collection_configuration.Stub( x => x.AddLink( link ) ).Return( configuration );
			};

			Because b = ( ) => result = sut.AddLink( link );

			It should_add_the_link_using_json_configuration = ( ) => result.ShouldBeTheSameAs( configuration );

			new static IJsonCollectionConfiguration result;
			static Link link;
			static IJsonCollectionConfiguration configuration;
		}

		public class when_adding_new_item : building_concern {
			Establish c = ( ) => {
				item_data_source_configuration = null;
				json_collection_configuration.Stub( x => x.AddItemsFor<object>( ) ).Return( item_data_source_configuration );
			};

			Because b = ( ) => result = sut.AddItemsFor<object>( );

			It should_add_the_item_using_json_configuration = ( ) => result.ShouldBeTheSameAs( item_data_source_configuration );

			new static IItemDataSourceConfiguration result;
			static IItemDataSourceConfiguration item_data_source_configuration;
		}

		public class when_adding_new_query : building_concern {
			Establish c = ( ) => {
				filter = new object( );
				query_configuration = null;
				json_collection_configuration.Stub( x => x.AddQueryFor( filter ) ).Return( query_configuration );
			};

			Because b = ( ) => result = sut.AddQueryFor( filter );

			It should_add_the_query_using_json_configuration = ( ) => result.ShouldBeTheSameAs( query_configuration );

			new static IQueryConfiguration result;
			static object filter;
			static IQueryConfiguration query_configuration;
		}

		public class when_setting_the_url : building_concern {
			Establish c = ( ) => { url = GetRandom.String( 12 ); };

			Because b = ( ) => sut.SetUrl( url );

			It should_set_it_using_json_configuration = ( ) => json_collection_configuration.WasToldTo( x => x.SetUrl( url ) );
		}
	}

}