using System.Collections.Generic;
using System.Reflection;
using AvenidaSoftware.Extensions;
using AvenidaSoftware.HypermediaTools.Extensions;

namespace AvenidaSoftware.HypermediaTools.Services {
	//TODO: test
	public class ObjectDataTemplateBuilderConfiguration : IDataTemplateBuilderConfiguration{
		public IEnumerable<DataTemplateBuilder> Create(PropertyInfo template_property_info) {
			var template_property_name = template_property_info.Name;
			var template_property_type = template_property_info.PropertyType;

			yield return data_source => {
				var data_template_builder = ItemInformationProvider.GetDataTemplateBuilder(template_property_type);
				var property_value = data_source.try_get_property_value(template_property_name);
				var data = data_template_builder(property_value);

				data.for_each( x => x.group_using(template_property_name) );

				return data;
			};
		}

		public bool Match(PropertyInfo template_property_info) {
			return true;
		}
	}

}