using System;
using System.Collections.Generic;
using System.Reflection;

namespace AvenidaSoftware.HypermediaTools.Services {
	//TODO: test
	public class CustomDataTemplateBuilderConfiguration : IDataTemplateBuilderConfiguration{
		static readonly Dictionary<Type, CustomDataBuilder> custom_data_builders = new Dictionary<Type, CustomDataBuilder>();

		public static void AddCustomDataBuilderFor<T>(CustomDataBuilder custom_data_builder) {
			custom_data_builders.Add(typeof (T), custom_data_builder);
		}

		public IEnumerable<DataTemplateBuilder> Create(PropertyInfo template_property_info) {
			yield return data_source => custom_data_builders[template_property_info.PropertyType](data_source);
		}

		public bool Match(PropertyInfo template_property_info) {
			return custom_data_builders.ContainsKey(template_property_info.PropertyType);
		}
	}
}