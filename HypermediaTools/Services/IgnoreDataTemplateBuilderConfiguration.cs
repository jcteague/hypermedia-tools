using System;
using System.Collections.Generic;
using System.Reflection;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	//TODO: test
	public class IgnoreDataTemplateBuilderConfiguration : IDataTemplateBuilderConfiguration{
		public List<Type> ignore_types = new List<Type> { typeof(Link), };

		public IEnumerable<DataTemplateBuilder> Create(PropertyInfo template_property_info) {
			yield return data_source => new List<Data>();
		}

		public bool Match(PropertyInfo template_property_info) {
			return ignore_types.Contains( template_property_info.PropertyType );
		}
	}

}