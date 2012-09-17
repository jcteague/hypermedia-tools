using System.Collections.Generic;

namespace HypermediaTools.Models {
    public class Error {
        public string title { get; set; }
        public string code { get; set; }
        public IEnumerable<string> messages { get; set; }
    }
}