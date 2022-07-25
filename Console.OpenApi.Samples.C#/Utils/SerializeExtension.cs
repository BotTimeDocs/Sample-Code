using Newtonsoft.Json;

namespace OpenApi.Sdk.Utils
{
    public static class SerializeExtension
    {
        public static string SerializeObject(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii,
                Formatting = Formatting.None,
            });
        }
    }
}