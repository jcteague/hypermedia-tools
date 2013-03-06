using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AvenidaSoftware.Extensions;

namespace AvenidaSoftware.HypermediaTools.Services {
	
	public class ItemConfiguration<TDataSource> : IItemConfiguration<TDataSource>{
		public Type TemplateType { get; private set; }
		public IEnumerable<TDataSource> DataSources { get; private set; }
		public ICollectionJsonConfiguration CollectionJsonConfiguration { get; private set; }
		public IDataBuilder DataBuilder { get; private set; }
		public List<Func<TDataSource, Link>> LinksBuilder { get; private set; }
		public List<Func<TDataSource, Data>> CustomsDataBuilder { get; private set; }
		public Func<TDataSource, Link> SelfLinkBuilder { get; private set; }

		public ItemConfiguration(IEnumerable<TDataSource> dataSources, Type templateType, ICollectionJsonConfiguration collectionJsonConfiguration, IDataBuilder dataBuilder) {
			TemplateType = templateType;
			DataSources = dataSources;
			CollectionJsonConfiguration = collectionJsonConfiguration;
			DataBuilder = dataBuilder;
			LinksBuilder = new List<Func<TDataSource, Link>>();
			CustomsDataBuilder = new List<Func<TDataSource, Data>>();

			var link_name = TemplateType.Name.pluralize();
			
			var attribute = TemplateType.GetCustomAttributes( false ).FirstOrDefault(x=>x.GetType(  ) == typeof(TemplateFilterAttribute)) as TemplateFilterAttribute;

			if(attribute!=null) {
				link_name = attribute.Name;
			}

			SelfLinkBuilder = ds => new Link{
				href = Collection.href,
				name = link_name,
				prompt = link_name.wordify()
			};
		}


		public ICollectionJsonConfiguration AddLink(Link link) {
			return CollectionJsonConfiguration.AddLink(link);
		}

		public IItemConfiguration<TDataSource> AddItemLink(Func<TDataSource, Link> link_builder) {
			LinksBuilder.Add(link_builder);
			return this;
		}

		public IItemConfiguration<TDataSource> AddCustomData(Data value) {
			return AddCustomData(x => value);
		}

		public IItemConfiguration<TDataSource> AddCustomData(Func<TDataSource, Data> data_template_builder) {
			CustomsDataBuilder.Add(data_template_builder);
			return this;
		}

		public IItemConfiguration<TDataSource> WithSelfLink(Func<TDataSource, Link> link_builder) {
			SelfLinkBuilder = link_builder;
			return this;
		}
		
		public void SetUrl(string url) {
			CollectionJsonConfiguration.SetUrl(url);
		}

		public Collection Collection {
			get { return CollectionJsonConfiguration.Collection; }
		}


		public IItemDataSourceConfiguration AddItemsFor<ItemModel>() {
			Build();
			return CollectionJsonConfiguration.AddItemsFor<ItemModel>();
		}

		public ITemplateDataSourceConfiguration AddTemplateFor<TTemplate>() {
			Build();
			return CollectionJsonConfiguration.AddTemplateFor<TTemplate>();
		}

		public IQueryConfiguration AddQueryFor(object filter) {
			Build();
			return CollectionJsonConfiguration.AddQueryFor(filter);
		}

		public CollectionJson Build() {
			var items = new List<Item>(Collection.items);
			foreach (var data_source in DataSources) {
				var item = ItemBuilder(TemplateType, data_source);

				var links = new List<Link>();

				foreach (var link_builder in LinksBuilder) {
					links.Add(link_builder(data_source));
				}

				var temp_data = new List<Data>(item.data);

				foreach (var custom_data_builder in CustomsDataBuilder) {
					temp_data.Add(custom_data_builder(data_source));
				}

				item.self = SelfLinkBuilder(data_source);
				item.links = links;
				item.data = temp_data;

				items.Add(item);
			}

			Collection.items = items;

			return CollectionJsonConfiguration.Build();
		}

		Item ItemBuilder(Type template_type, object data_source) {
			var data = new List<Data>(DataBuilder.GetDatasFor(template_type, data_source));
			var embedded_type_infos = TemplateType.get_public_writable_instance_properties().Where(x => x.PropertyType.Namespace=="System.Collections.Generic");
			var embedded_collection = new List<EmbeddedCollection>();

			foreach (var embedded_type_info in embedded_type_infos) {
				var property_name = embedded_type_info.Name;
				var property_value = data_source.try_get_property_value(property_name);
				var embedded_items = new List<Item>();

				foreach (var element in (IEnumerable) property_value) {
					var embedded_item = new Item();
					embedded_item.data = DataBuilder.GetDatasFor(element.GetType(), element);
					embedded_item.links = CreateLinks(element);
					embedded_items.Add(embedded_item);
				}

				embedded_collection.Add( new EmbeddedCollection{ name = property_name, items = embedded_items} );
			}

			var item = new Item{
				data = data,
				links = new List<Link>(),
				embedded = embedded_collection
			};

			return item;
		}

		IEnumerable<Link> CreateLinks( object element ) {
			var properties = element.GetType(  ).get_public_writable_instance_properties().Where(x => x.PropertyType.inherits_from<Link>(  ));
			return properties.Select( property_info => property_info.GetValue( element, null ) as Link );
		}
	}

}