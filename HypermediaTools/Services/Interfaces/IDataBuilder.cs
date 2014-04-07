using System;
using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {

	public interface IDataBuilder {
		 IEnumerable<Data> GetDatasFor(Type template_type, object data_source);
	}

}