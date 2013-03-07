using System;
using System.Collections.Generic;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public interface ITemplateConfiguration< out TDataSource> : IJsonCollectionConfiguration{
		TDataSource DataSource { get; }

		ITemplateConfiguration<TDataSource> AddCustomData( Data data );
		ITemplateConfiguration<TDataSource> AddCustomData( Func<TDataSource, Data> data_template_builder);
		ITemplateConfiguration<TDataSource> AddCustomData( Func<TDataSource, IEnumerable<Data>> data_template_builder );
	}

}