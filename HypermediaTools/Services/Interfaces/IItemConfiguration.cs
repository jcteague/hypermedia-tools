using System;

namespace AvenidaSoftware.HypermediaTools.Services {

	public interface IItemConfiguration<TDataSource> : IJsonCollectionConfiguration{
		IItemConfiguration<TDataSource> AddItemLink(Func<TDataSource, Link> link_builder);
		IItemConfiguration<TDataSource> AddCustomData(Data value);
		IItemConfiguration<TDataSource> AddCustomData(Func<TDataSource, Data> data_template_builder);
		IItemConfiguration<TDataSource> WithSelfLink(Func<TDataSource, Link> link_builder);
	}

}