using System.Linq;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using FizzWare.NBuilder.Generators;
using Machine.Specifications;
using NHibernate.Linq;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {

	[Subject(typeof(JsonCollectionConfiguration))]
	public class JsonCollectionConfigurationSpecs {

		public abstract class concern : Observes<IJsonCollectionConfiguration, JsonCollectionConfiguration>{
			Establish c = () => {
				DataBuilder = depends.on<IDataBuilder>();
			};

			protected static IDataBuilder DataBuilder;
		}

		public class when_setting_the_url_to_a_collection : concern{
			Establish c = () => {
				url = GetRandom.String(32);
			};

			Because b = () => sut.SetUrl(url);

			It should_set_the_collection_href = () => sut.As<JsonCollectionConfiguration>().Collection.href.ShouldEqual(url);

			static string url;		
		}

		public class when_building_the_collection : concern{
			Establish c = () => {
				url = GetRandom.String(32);
			};

			Because b = () => result = sut.Build();

			It should_return_the_correct_collection = () => result.collection.ShouldBeTheSameAs(sut.As<JsonCollectionConfiguration>().Collection);

			static string url;
			static JsonCollection result;
		}

		public class when_adding_a_new_item_for_given_template: concern{
			Because b = () => result = sut.AddItemsFor<object>();

			It should_return_a_new_item_configuration = () => {
				var item_configuration = result.As<ItemDataSourceConfiguration>();
				item_configuration.TemplateType.ShouldEqual(typeof (object));
				item_configuration.JsonCollectionConfiguration.ShouldBeTheSameAs(sut);
				item_configuration.DataBuilder.ShouldBeTheSameAs(DataBuilder);
			};

			static IItemDataSourceConfiguration result;
		}

		public class when_adding_a_new_template_for_given_model: concern{
			Because b = () => result = sut.AddTemplateFor<object>();

			It should_return_a_new_item_configuration = () => {
				var item_configuration = result.As<TemplateDataSourceConfiguration>();
				item_configuration.TemplateType.ShouldEqual(typeof (object));
				item_configuration.Configuration.ShouldBeTheSameAs(sut);
				item_configuration.DataBuilder.ShouldBeTheSameAs(DataBuilder);
			};

			static ITemplateDataSourceConfiguration result;
		}

		public class when_adding_new_link_to_the_collection:concern{
			Establish c = () => {
				link =new Link();
			};

			Because b = () => {
				sut.AddLink(new Link());
				sut.AddLink(link);
			};

			It should_add_the_link_correctly = () => sut.Collection.links.ShouldContain(link);

			It should_not_remove_previously_added_links = () => sut.Collection.links.Count().ShouldEqual(2);

			static Link link;
		}

		public class when_adding_a_new_querty_template: concern{
			Establish c = () => {
				query_model = new object();
			};

			Because b = () => result = sut.AddQueryFor(query_model);

			It should_return_a_new_item_configuration = () => {
				var item_configuration = result.As<QueryConfiguration>();
				item_configuration.Filter.ShouldEqual(query_model);
				item_configuration.Configuration.ShouldBeTheSameAs(sut);
				item_configuration.DataBuilder.ShouldBeTheSameAs(DataBuilder);
			};

			static IQueryConfiguration result;
			static object query_model;
		}
	}
}