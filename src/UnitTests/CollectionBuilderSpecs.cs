using System.Collections.ObjectModel;
using HypermediaTools.CollectionBuilders;
using developwithpassion.specifications.rhinomocks;

namespace HyperMediaTools.UnitTests
{
    public class SampleObject : IAmAResource
    {
        public override string GetIdentifer()
        {
            throw new System.NotImplementedException();
        }

        public string GetResourceName()
        {
            return "Sample";
        }
    }
    


}