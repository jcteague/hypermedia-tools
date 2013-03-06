using System.ComponentModel.DataAnnotations;
using AvenidaSoftware.HypermediaTools.Extensions;

namespace AvenidaSoftware.HypermediaTools {

	public class Data {

		[ Required ]
		public string name { get; set; }

		public string value { get; set; }

		public string prompt { get; set; }

		public string type { get; set; }

		public string acceptable_values { get; set; }

		public string id {
			get { return name.Replace( "[", "" ).Replace( "]", "" ).wordify( "_" ); }
		}

		public Data Clone() {
			return new Data{
				name	= name,
				value = value,
				prompt = prompt,
				acceptable_values = acceptable_values,
				type = type
			};
		}
	}

}