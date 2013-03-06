namespace AvenidaSoftware.HypermediaTools.Services {

	public interface ITemplateDataSourceConfiguration {
		ITemplateConfiguration<TDataSource> UseDataSource<TDataSource>(TDataSource data_source);
		CollectionJson Build();
	}

}