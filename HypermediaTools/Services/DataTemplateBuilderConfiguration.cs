using System;
using System.Collections.Generic;
using System.Reflection;
using AvenidaSoftware.Extensions;
using AvenidaSoftware.HypermediaTools.Extensions;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	//TODO: test
	public class DataTemplateBuilderConfiguration : IDataTemplateBuilderConfiguration{
		static readonly Dictionary<Type, string> data_type_dictionary = new Dictionary<Type, string>{
			{typeof (bool), "boolean"},
			{typeof (DateTime), "datepicker"},
			{typeof (DateTimeOffset), "datepicker"}
		};

		public static string GetDataType(Type template_property_type) {
			var data_type = "string";
			var key_property_type = template_property_type.un_box_type();

			if (data_type_dictionary.ContainsKey(key_property_type)) {
				data_type = data_type_dictionary[key_property_type];
			}

			return data_type;
		}

		public IEnumerable<DataTemplateBuilder> Create(PropertyInfo template_property_info) {
			var template_property_name = template_property_info.Name;
			var template_property_type = template_property_info.PropertyType;

			var data_type = GetDataType(template_property_type);

			var data = new Data{
				name = template_property_name,
				prompt = template_property_name.wordify_field(),
				type = data_type,
			};

			var filter_template_attribute = template_property_info.get_attribute<TemplateFilterAttribute>();

			if (filter_template_attribute != null) {
				data.name = filter_template_attribute.Name ?? data.name;
				data.value = filter_template_attribute.Value ?? data.value;
				data.prompt = filter_template_attribute.Prompt ?? data.prompt;
				data.type = filter_template_attribute.Type ?? data.type;

				if (filter_template_attribute.Group.is_not_null_nor_empty()) data.group_using(filter_template_attribute.Group);
			}

			yield return data_source => {
				var property_value = data_source.try_get_property_value( template_property_name );
				var result = data.Clone();
				
				result.value = property_value != null ? property_value.ToString() : "";

				return new List<Data> {result};
			};

		}

		public bool Match(PropertyInfo template_property_info) {
			var template_property_type = template_property_info.PropertyType;

			return template_property_type.IsValueType || template_property_type == typeof(string);
		}
	}
}