using System.Collections.Generic;

namespace HypermediaTools.Models {
    public class Template {
        
        public List<dynamic> data { get; set; }

        public Template( ) {
            data = new List<dynamic>( );
        }
    }
}