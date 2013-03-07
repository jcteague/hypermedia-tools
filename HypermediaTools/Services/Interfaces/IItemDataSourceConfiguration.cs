using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public interface IItemDataSourceConfiguration {
		IItemConfiguration<TDataSource> UseDataSource<TDataSource>(IEnumerable<TDataSource> data_sources);
		IItemConfiguration<TDataSource> UseDataSource<TDataSource>(TDataSource data_source);
	}

}