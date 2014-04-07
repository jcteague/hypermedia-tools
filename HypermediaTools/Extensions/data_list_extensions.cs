using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Extensions {

	public static class data_list_extensions{
		public static Data group_using(this Data data, string wrapper) {
			data.name = string.Format("{0}[{1}]", wrapper, data.name);
			return data;
		}

		public static IEnumerable<Data> group_using(this IEnumerable<Data> data, string wrapper) {
			foreach (var inner_data in data) {
				yield return inner_data.group_using(wrapper);
			}
		}
	}

}