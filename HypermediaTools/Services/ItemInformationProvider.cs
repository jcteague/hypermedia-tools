using System;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.Extensions;

namespace AvenidaSoftware.HypermediaTools.Services {
	//TODO: test
	public class ItemInformationProvider{

		static readonly Dictionary<Type, DataTemplateBuilder> data_template_builders = new Dictionary<Type, DataTemplateBuilder>();

		static readonly IList<IDataTemplateBuilderConfiguration> data_template_builder_configurations = new List<IDataTemplateBuilderConfiguration>{
			new CustomDataTemplateBuilderConfiguration(),
			new IgnoreDataTemplateBuilderConfiguration(),
			new DataTemplateBuilderConfiguration(),
			new ObjectDataTemplateBuilderConfiguration()
		};
		
		public static DataTemplateBuilder GetDataTemplateBuilder(Type template_type) {
			if (data_template_builders.ContainsKey(template_type)) return data_template_builders[template_type];

			var property_infos = template_type.get_public_writable_instance_properties().Where( x=>!x.PropertyType.inherits_from<Link>() );
			var builder_list = new List<DataTemplateBuilder>();

			foreach (var template_property_info in property_infos) {
				var data_template_builder_configuration = data_template_builder_configurations.First(x => x.Match(template_property_info));
				builder_list.AddRange(data_template_builder_configuration.Create(template_property_info));
			}

			DataTemplateBuilder data_template_builder = data_source => {
				var result = new List<Data>();

				foreach (var builder in builder_list) {
					result.AddRange(builder(data_source));
				}

				return result;
			};

			data_template_builders.try_add( template_type, data_template_builder );

			return data_template_builder;
		}

	}

}