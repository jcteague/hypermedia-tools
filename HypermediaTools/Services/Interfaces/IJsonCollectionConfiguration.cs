namespace AvenidaSoftware.HypermediaTools.Services {

	public interface IJsonCollectionConfiguration {
		JsonCollection Build();
		IItemDataSourceConfiguration AddItemsFor<ItemModel>();
		ITemplateDataSourceConfiguration AddTemplateFor<TemplateModel>();
		IQueryConfiguration AddQueryFor(object filter);
		IJsonCollectionConfiguration AddLink(Link link);
		void SetUrl(string url);
		Collection Collection { get; }
	}

}