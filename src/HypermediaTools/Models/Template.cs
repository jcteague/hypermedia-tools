using System.Collections.Generic;

namespace HypermediaTools.Models {
    public class Template {
        
        public IEnumerable<Data> data { get; set; }

        public Template( ) {
            data = new List<Data>( );
        }
    }
}