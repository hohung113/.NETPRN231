using System.Text.Json;

namespace eStoreClient.JsonHelper
{
    public static class JsonHelperOption
    {
        public static JsonSerializerOptions DefaultOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public static List<T> DeserializeList<T>(string json)
        {
            var options = DefaultOptions();
            return JsonSerializer.Deserialize<List<T>>(json, options);
        }
    }
}
