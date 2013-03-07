using System;
using System.Collections.Generic;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using Machine.Fakes;
using Machine.Specifications;
using NHibernate.Linq;
using Rhino.Mocks;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {
	
	[Subject(typeof(TemplateDataSourceConfiguration))]
	public class TemplateDataSourceConfigurationSpecs{

		public abstract class concern : Observes<ITemplateDataSourceConfiguration, TemplateDataSourceConfiguration>{
			Establish c = () => {
				template_type = depends.on(typeof (object));
				json_collection_configuration = depends.on<IJsonCollectionConfiguration>();
				DataBuilder = depends.on<IDataBuilder>();
			};

			protected static Type template_type;
			protected static IJsonCollectionConfiguration json_collection_configuration;
			protected static IDataBuilder DataBuilder;
		}

		public class when_setting_the_data_source : concern{
			Establish c = () => { test_data_source = new TestData(); };

			Because b = () => { result = sut.UseDataSource(test_data_source); };

			It should_return_an_item_configuration= () => {
				var item_configuration = result.As<TemplateConfiguration<TestData>>();
				item_configuration.DataSource.ShouldBeTheSameAs(test_data_source);
				item_configuration.TemplateType.ShouldBeTheSameAs(template_type);
				item_configuration.DataBuilder.ShouldBeTheSameAs(DataBuilder);
			};

			static ITemplateConfiguration<TestData> result;
			static TestData test_data_source;
		}

		public class when_building_the_collection: concern{

			Establish c = () => {
				var obj_to_return = new Collection { template = new Template { data = new List<Data>( ) } };
				json_collection_configuration.Stub(x => x.Collection).Return( obj_to_return );
				DataBuilder.Stub(x => x.GetDatasFor(Arg<Type>.Is.Equal(typeof (object)), Arg<object>.Is.Anything)).Return(new List<Data>());
			};

			Because b = () => sut.Build();

			It should_build_it_using_the_json_configuration = () => json_collection_configuration.WasToldTo(x => x.Build());
		}
	}
}