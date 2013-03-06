using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class QueryConfiguration : IQueryConfiguration{
		public ICollectionJsonConfiguration JsonConfiguration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }
		public object Filter { get; private set; }

		public QueryConfiguration(ICollectionJsonConfiguration json_configuration, IDataBuilder data_builder, object filter) {
			JsonConfiguration = json_configuration;
			DataBuilder = data_builder;
			this.Filter = filter;
		}

		public ICollectionJsonConfiguration WithSortingFields<SortingModel>() {
			var query = new Query();
			var collection = JsonConfiguration.Collection;

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

			return JsonConfiguration;
		}
	}

}