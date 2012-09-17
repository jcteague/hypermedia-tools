using System.Collections.Generic;

namespace HypermediaTools.Models
{
    public class CollectionModel
    {
        public Template template { get; set; }

        public Link self { get; set; }

        List<Link> _links = new List<Link>();
        public IEnumerable<Link> links
        {
            get { return _links; }
            
        }

        List<CollectionItem> _items = new List<CollectionItem>();
        public IEnumerable<CollectionItem> items
        {
            get { return _items; }
            
        }

        public void AddCollectionItem(CollectionItem dataItem)
        {
            _items.Add(dataItem);
        }

        public void AddCollectionLink(Link link)
        {
            _links.Add(link);
        }
    }

}