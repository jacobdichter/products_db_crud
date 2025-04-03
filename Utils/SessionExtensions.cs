using System.Text.Json;

namespace ProductsAppRP.Utils
{
    public static class SessionExtensions // All methods are static
    {
        //Extend ISession for generic Set<T> and Get<T> type-safe access to Session
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
