using System;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.Extensions;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using FizzWare.NBuilder.Generators;
using Machine.Fakes;
using Machine.Specifications;
using Rhino.Mocks;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {

	[ Subject( typeof( ItemConfiguration<TestData> ) ) ]
	public class ItemConfigurationSpecs {
		public abstract class concern : Observes<IItemConfiguration<TestData>, ItemConfiguration<TestData>> {
			Establish c = ( ) => {
				template_type = depends.on( typeof( TestData ) );
				json_collection_configuration = depends.on<IJsonCollectionConfiguration>( );
				DataBuilder = depends.on<IDataBuilder>( );
			};

			protected static Type template_type;
			protected static IJsonCollectionConfiguration json_collection_configuration;
			protected static IDataBuilder DataBuilder;
		}

		public class building_concern : concern {
			Establish c = ( ) => {
				var test_link = new Link { href = "href", name = "name" };

				test_data_source = new TestData { Name = GetRandom.String( 10 ), EmbeddedData = new List<TestEmbedded> { new TestEmbedded { Number = 1, TestLink = test_link } } };

				depends.on<IEnumerable<TestData>>( new List<TestData> { test_data_source } );

				url = GetRandom.String( 10 );
				collection = new Collection { href = url, items = new List<Item>( ) };
				json_collection_configuration.Stub( x => x.Collection ).Return( collection );
				datas = new List<Data> { test_data };


				DataBuilder.Stub( x => x.GetDatasFor( template_type, test_data_source ) ).Return( datas );
				test_embedded_data = new Data { name = "embedded" };
				embedded_datas = new List<Data> { test_embedded_data };
				DataBuilder.Stub( x => x.GetDatasFor( typeof( TestEmbedded ), test_data_source.EmbeddedData.First( ) ) ).Return( embedded_datas );
				json_collection_configuration.Stub( x => x.Build( ) ).Return( json_collection );

				custom_data = new Data { name = "Custom" };
			};

			protected static TestData test_data_source;
			protected static Collection collection;
			protected static string url;
			protected static IEnumerable<Data> datas;
			protected static JsonCollection json_collection;
			protected static Data custom_data;
			protected static Data test_data;
			protected static IEnumerable<Data> embedded_datas;
			protected static Data test_embedded_data;
		}


		public class when_building_items_collection_with_item_links_and_custom_data : building_concern {
			Because b = ( ) => {
				sut.AddItemLink( x => new Link { name = x.Name } );
				sut.AddCustomData( custom_data );
				result = sut.Build( );
			};

			It should_return_the_correct_items = ( ) => {
				var item = collection.items.ElementAt( 0 );
				item.self.href.ShouldEqual( url );
				item.self.name.ShouldEqual( typeof( TestData ).Name.pluralize( ) );
				item.data.ElementAt( 0 ).ShouldBeTheSameAs( test_data );
				item.data.ElementAt( 1 ).ShouldBeTheSameAs( custom_data );
				item.links.First( ).name.ShouldEqual( test_data_source.Name );
			};

			It should_return_the_collection_json_of_json_configuration = ( ) => result.ShouldBeTheSameAs( json_collection );

			It should_add_the_embedded_collection = ( ) => {
				var item = collection.items.First( );
				var embedded_items = item.embedded.First( ).items;
				var embedded_data = embedded_items.First( ).data;
				embedded_data.Count( ).ShouldEqual( 1 );
				embedded_data.ShouldBeTheSameAs( embedded_data );
				embedded_items.First( ).links.Count( ).ShouldEqual( 1 );
			};

			static JsonCollection result;
		}

		public class when_adding_new_link_to_the_collection : building_concern {
			Establish c = ( ) => {
				link = new Link( );
				configuration = null;
				json_collection_configuration.Stub( x => x.AddLink( link ) ).Return( configuration );
			};

			Because b = ( ) => result = sut.AddLink( link );

			It should_add_the_link_using_json_configuration = ( ) => result.ShouldBeTheSameAs( configuration );

			static IJsonCollectionConfiguration result;
			static Link link;
			static IJsonCollectionConfiguration configuration;
		}

		public class when_adding_new_link : building_concern {
			Establish c = ( ) => {
				item_data_source_configuration = null;
				json_collection_configuration.Stub( x => x.AddItemsFor<object>( ) ).Return( item_data_source_configuration );
			};

			Because b = ( ) => result = sut.AddItemsFor<object>( );

			It should_add_the_item_using_json_configuration = ( ) => result.ShouldBeTheSameAs( item_data_source_configuration );

			static IItemDataSourceConfiguration result;
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

			static IQueryConfiguration result;
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