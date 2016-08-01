using Newtonsoft.Json;

namespace AudioSharp.Utils
{
    public static class JsonUtils
    {
        public static string SerializeObject(object objectToSerialize)
        {
            return JsonConvert.SerializeObject(objectToSerialize);
        }

        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static object DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
