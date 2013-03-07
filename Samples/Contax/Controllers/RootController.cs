using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using HypermediaTools.Models;

namespace Contax.Api.Controllers
{
    public class RootController : ApiController {
        public dynamic Get() {
            var root_model = new CollectionModel();
            root_model.self = new Link{href = "/"};

            root_model.AddCollectionLink(new Link { href = "/contacts", name = "Contacts", prompt = "Contacts", rel = "Contacts" });
            return new {collection = root_model};
        }
    }
}