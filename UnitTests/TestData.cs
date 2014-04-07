using System.Collections.Generic;

namespace HypermediaTools.UnitTests {
	public class TestData{
		public string Name { get; set; }
		public IEnumerable<TestEmbedded> EmbeddedData {get;set;}
	}
}