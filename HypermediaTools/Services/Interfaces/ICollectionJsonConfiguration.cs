namespace AvenidaSoftware.HypermediaTools.Services {

	public interface ICollectionJsonConfiguration {
		CollectionJson Build();
		IItemDataSourceConfiguration AddItemsFor<ItemModel>();
		ITemplateDataSourceConfiguration AddTemplateFor<TemplateModel>();
		IQueryConfiguration AddQueryFor(object filter);
		ICollectionJsonConfiguration AddLink(Link link);
		void SetUrl(string url);
		Collection Collection { get; }
	}

}