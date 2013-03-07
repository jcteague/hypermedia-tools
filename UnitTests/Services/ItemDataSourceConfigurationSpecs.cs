using System;
using System.Linq;
using AvenidaSoftware.HypermediaTools.Services;
using Machine.Specifications;
using NHibernate.Linq;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {

	[Subject(typeof (ItemDataSourceConfiguration))]
	public class ItemDataSourceConfigurationSpecs{
		
		public abstract class concern : Observes<IItemDataSourceConfiguration, ItemDataSourceConfiguration>{
			Establish c = () => {
				template_type = depends.on(typeof (object));
				json_collection_configuration = depends.on<IJsonCollectionConfiguration>();
				data_builder = depends.on<IDataBuilder>();
			};

			protected static Type template_type;
			protected static IJsonCollectionConfiguration json_collection_configuration;
			protected static IDataBuilder data_builder;
		}


		public class when_setting_the_data_source : concern{
			Establish c = () => { test_data_source = new TestData(); };

			Because b = () => { result = sut.UseDataSource(test_data_source); };

			It should_return_an_item_configuration= () => {
				var item_configuration = result.As<ItemConfiguration<TestData>>();
				item_configuration.DataSources.First().ShouldBeTheSameAs(test_data_source);
				item_configuration.TemplateType.ShouldBeTheSameAs(template_type);
				item_configuration.DataBuilder.ShouldBeTheSameAs(data_builder);
			};

			static IItemConfiguration<TestData> result;
			static TestData test_data_source;
		}
	}
}