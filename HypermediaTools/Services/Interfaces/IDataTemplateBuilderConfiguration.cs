using System.Collections.Generic;
using System.Reflection;

namespace AvenidaSoftware.HypermediaTools.Services {

	public interface IDataTemplateBuilderConfiguration {
		IEnumerable<DataTemplateBuilder> Create(PropertyInfo template_property_info);
		bool Match(PropertyInfo template_property_info);	 
	}

}