using System;
using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	public delegate IEnumerable<Data> CustomDataBuilder(object datasource);

	public class DataBuilder : IDataBuilder {
		public IEnumerable<Data> GetDatasFor(Type template_type, object data_source) {
			var data_template_builder = ItemInformationProvider.GetDataTemplateBuilder(template_type);

			return data_template_builder(data_source);
		}
	}

}