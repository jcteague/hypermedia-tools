using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace HypermediaTools.Models {
    public class Item {
        public Link self { get; set; }
        public IEnumerable<Data> data { get; set; }
        public IEnumerable<Link> links { get; set; }

        public Item( ) {
            links = new List<Link>( );
        }

        public static HttpResponseMessage For< T >( IEnumerable<Data> data, Guid entity_id ) {
            var item = new Item { data = data, self = new Link { href = string.Format( "/{0}/{1}", typeof( T ).Name, entity_id ), prompt = "View Details" } };


            var http_response_message = new HttpResponseMessage( HttpStatusCode.Created ) { Content = new ObjectContent( item.GetType( ), item, new JsonMediaTypeFormatter( ) ) };
            return http_response_message;
        }
    }
}