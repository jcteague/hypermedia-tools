using System.Collections.Generic;

namespace HypermediaTools.Models
{
    public class CollectionItem
    {
        public string Href { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
        public IEnumerable<Link> Links { get; set; } 
    }
}