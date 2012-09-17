using System.Collections.Generic;

namespace HypermediaTools.Models
{
    public class CollectionItem
    {
        public string Href { get; set; }
        public IEnumerable<Data> Data { get; set; }
        public IEnumerable<Link> Links { get; set; } 
    }
}