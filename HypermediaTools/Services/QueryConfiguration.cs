using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class QueryConfiguration : IQueryConfiguration{
		public IJsonCollectionConfiguration Configuration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }
		public object Filter { get; private set; }

		public QueryConfiguration(IJsonCollectionConfiguration configuration, IDataBuilder data_builder, object filter) {
			Configuration = configuration;
			DataBuilder = data_builder;
			this.Filter = filter;
		}

		public IJsonCollectionConfiguration WithSortingFields<SortingModel>() {
			var query = new Query();
			var collection = Configuration.Collection;

			var filter = query.filter;
			filter.href = collection.href;
			filter.data = DataBuilder.GetDatasFor(Filter.GetType(), Filter);

			var sort = query.sort;
			sort.href = collection.href;
			sort.data = DataBuilder.GetDatasFor(typeof(SortingModel), new object());

			var queries = new List<Query>();
			
			if(collection.queries != null)queries.AddRange(collection.queries);

			queries.Add(query);
			collection.queries = queries;

			return Configuration;
		}
	}

}