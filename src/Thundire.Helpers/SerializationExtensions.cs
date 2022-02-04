using System.Text.Json;

namespace Thundire.Helpers
{
    public static class SerializationExtensions
    {
        public static T? JsonSerializationDeepCopy<T>(this T self) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(self));
    }
}