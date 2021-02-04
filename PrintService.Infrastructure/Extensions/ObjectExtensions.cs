using Newtonsoft.Json;

namespace PrintService.Infrastructure.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object value, Formatting formatting = Formatting.None)
        {
            if (value == null)
                return null;

            return JsonConvert.SerializeObject(value, formatting, new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            });
        }

        public static T ToObject<T>(this string value)
            where T : class
        {
            if (value == null)
                return null;

            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            });
        }
    }
}
