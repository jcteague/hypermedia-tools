using System;
using System.Collections.Generic;
using HypermediaTools.Attributes;
using HypermediaTools.CollectionBuilders;

namespace contax.Models
{
    public class Contact : IAmAResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string TwitterAccount { get; set; }
        [EmbeddedResource]
        public IEnumerable<Tag> Tags { get; set; }
        public override string GetIdentifer()
        {
            return Id.ToString();
        }
    }

    public class Tag : IAmAResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string GetIdentifer()
        {
            return Id.ToString();
        }
    }
    public class Conversation : IAmAResource
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public override string GetIdentifer()
        {
            return Id.ToString();
        }
    }
}