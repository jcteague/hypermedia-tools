using System.Collections.Generic;
using contax.Models;

namespace Contax.Services
{
    public interface IContactRepository {
        Contact GetContact(int id);
        IEnumerable<Contact> Get(int page, int rows);
    }

}
