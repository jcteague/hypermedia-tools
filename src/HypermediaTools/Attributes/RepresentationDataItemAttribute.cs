using System;

namespace HypermediaTools.Attributes
{
    public class RepresentationDataItemAttribute : Attribute
    {
        public string Name { get; set; }

        public string Prompt { get; set; }
    }
}