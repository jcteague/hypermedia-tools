using System.Web.Http;
using Contax.Services;
using contax.Models;

namespace Contax.Api.Controllers
{
    public class ContactsController : ApiController
    {
        IContactRepository contactRepository;
        readonly IBuildCollection<Contact> collectionBuilder;
        IBuildCollectionItems<Contact> resource_builder;

        public ContactsController(IContactRepository contactRepository,IBuildCollectionItems<Contact> resource_builder, IBuildCollection<Contact> collectionBuilder)
        {
            this.contactRepository = contactRepository;
            this.collectionBuilder = collectionBuilder;
            this.resource_builder = resource_builder;
        }
        
        public dynamic Get()
        {
            var collection_model = collectionBuilder.BuildCollection("/", contactRepository.Get(1, 25));
            return new {collection = collection_model};


        }
        
    }
    public class ContactController : ApiController
    {
        IContactRepository contactRepository;
        IBuildCollectionItems<Contact > resource_builder;

        public ContactController(IContactRepository contactRepository, IBuildCollectionItems<Contact> resourceBuilder)
        {
            this.contactRepository = contactRepository;
            resource_builder = resourceBuilder;
        }
        
        public dynamic Get(int id)
        {
            var contact = contactRepository.GetContact(id);
            var resource = resource_builder.GetCollectionItems(contact);
            return new { resource = resource };
        }
    }
}