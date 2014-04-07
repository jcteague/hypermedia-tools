using System;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.HypermediaTools;
using AvenidaSoftware.HypermediaTools.Services;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace HypermediaTools.UnitTests.Services {

	[Subject(typeof (DataBuilder))]
	public class DataBuilderSpecs{
		public abstract class concern : Observes<IDataBuilder, DataBuilder>{}
		
		public class when_building_a_dat_for_a_model_type_with_two_properties : concern{
			Because b = () => result = sut.GetDatasFor(typeof (TestClass), null);

			It should_return_a_data_for_test_property_1 = () => {
				var data = result.ElementAt(0);
				data.name.ShouldEqual("test_property1");
				data.type.ShouldEqual("string");
			};

			It should_return_a_data_for_test_property_2 = () => {
				var data = result.ElementAt(1);
				data.name.ShouldEqual("test_property2");
				data.type.ShouldEqual("string");
			};

			It should_return_a_data_for_boolean_property = () => {
				var data = result.ElementAt(2);
				data.name.ShouldEqual("test_bool_property");
				data.type.ShouldEqual("boolean");
			};

			It should_return_a_data_for_nullable_boolean_property = () => {
				var data = result.ElementAt(3);
				data.name.ShouldEqual("test_nullable_bool_property");
				data.type.ShouldEqual("boolean");
			};

			It should_return_a_data_for_date_time_teproperty = () => {
				var data = result.ElementAt(4);
				data.name.ShouldEqual("test_date_time_property");
				data.type.ShouldEqual("datepicker");
			};

			It should_return_a_data_for_nullable_date_time_property = () => {
				var data = result.ElementAt(5);
				data.name.ShouldEqual("test_nullable_date_time_property");
				data.type.ShouldEqual("datepicker");
			};

			It should_return_a_data_for_date_time_off_set_teproperty = () => {
				var data = result.ElementAt(6);
				data.name.ShouldEqual("test_date_time_offset_property");
				data.type.ShouldEqual("datepicker");
			};

			It should_return_a_data_for_nullable_date_time_off_set_property = () => {
				var data = result.ElementAt(7);
				data.name.ShouldEqual("test_nullable_date_time_offset_property");
				data.type.ShouldEqual("datepicker");
			};

			static IEnumerable<Data> result;
		}

		public class when_building_a_template_for_a_model_type_with_complex_property : concern{
			Because b = () => result = sut.GetDatasFor(typeof (TestComplexClass), null);

			It should_return_a_data_for_test_property_1 = () => {
				var data = result.ElementAt(0);
				data.name.ShouldEqual("test_property1");
				data.type.ShouldEqual("string");
			};

			It should_return_a_data_for_test_property_2_with_complex_property1 = () => {
				var data = result.ElementAt(1);
				data.name.ShouldEqual("test_property2[complex_property1]");
				data.type.ShouldEqual("string");
			};

			It should_return_a_data_for_test_property_2_with_complex_property2 = () => {
				var data = result.ElementAt(2);
				data.name.ShouldEqual("test_property2[complex_property2]");
				data.type.ShouldEqual("string");
			};

			static IEnumerable<Data> result;
		}

		public class when_building_a_template_for_a_model_type_and_the_datasource_does_contain_all_the_properties : concern{
			Because b = () => {
				var data_source = new TestComplexClass{
					test_property1 = "blahh",
					test_property2 = new TestProperty{ complex_property1 = "property1" }
				};

				result = sut.GetDatasFor(typeof (TestComplexClass), data_source );
			};

			It should_return_a_data_for_test_property_1 = () => {
				var data = result.ElementAt(0);
				data.name.ShouldEqual("test_property1");
				data.type.ShouldEqual("string");
				data.value.ShouldEqual("blahh");
			};

			It should_return_a_data_for_test_property_2_with_complex_property1 = () => {
				var data = result.ElementAt(1);
				data.name.ShouldEqual("test_property2[complex_property1]");
				data.type.ShouldEqual("string");
				data.value = "property1";
			};

			It should_return_a_data_for_test_property_2_with_complex_property2 = () => {
				var data = result.ElementAt(2);
				data.name.ShouldEqual("test_property2[complex_property2]");
				data.type.ShouldEqual("string");
			};

			static IEnumerable<Data> result;
		}

		public class when_building_a_template_for_custom_type : concern{
			Establish c = () => {
				var item = new Data { name = "custom_data" };
				CustomDataTemplateBuilderConfiguration.AddCustomDataBuilderFor<CustomType>( data_source => new List<Data>{item} );
			};

			Because b = () => result = sut.GetDatasFor(typeof (TestCustomType),new TestCustomType());

			It should_return_a_data_for_custom_property = () => {
				var data = result.ElementAt(0);
				data.name.ShouldEqual("custom_data");
			};

			static IEnumerable<Data> result;
		}


		public class TestClass{
			public string test_property1 { get; set; }
			public string test_property2 { get; set; }
			public bool test_bool_property { get; set; }
			public bool? test_nullable_bool_property { get; set; }

			public DateTime test_date_time_property { get; set; }
			public DateTime? test_nullable_date_time_property { get; set; }
			public DateTimeOffset test_date_time_offset_property { get; set; }
			public DateTimeOffset? test_nullable_date_time_offset_property { get; set; }
		}

		public class TestComplexClass{
			public string test_property1 { get; set; }
			public TestProperty test_property2 { get; set; }
		}

		public class TestProperty{
			public string complex_property1 { get; set; }
			public string complex_property2 { get; set; }
		}
		public class TestCustomType{
			public CustomType CustomProperty { get; set; }
		}

		public class CustomType{
			public string complex_property1 { get; set; }
			public string complex_property2 { get; set; }
		}
	}
}