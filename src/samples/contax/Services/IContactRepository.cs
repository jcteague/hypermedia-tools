using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using contax.Models;

namespace Contax.Services
{
    public interface IContactRepository {
        Contact GetContact(int id);
        IEnumerable<Contact> Get(int page, int rows);
    }

    class ContactRepository : IContactRepository
    {
        static IList<Contact> _data;

        static ContactRepository()
        {
            
            _data = Builder<Contact>.CreateListOfSize(50)
                .All()
                    .With(c=>c.Tags = Builder<Tag>.CreateListOfSize(3).Build())
                .Build();

        }
        public Contact GetContact(int id)
        {
            return _data.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> Get(int page = 1, int rows = 20)
        {
            return _data.Skip(page*rows).Take(rows);
        }
    }
}
