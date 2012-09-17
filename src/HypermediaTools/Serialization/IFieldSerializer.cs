using Newtonsoft.Json;

namespace HypermediaTools.Serialization
{
    public interface IFieldSerializer
    {
        string Serialize(object fieldValue);
    }
    public class JsonFieldSerializer : IFieldSerializer
    {
        public string Serialize(object fieldValue)
        {
            return 
                JsonConvert.SerializeObject(fieldValue);
        }
    }
}