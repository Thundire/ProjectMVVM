using System.Text.Json;

namespace Thundire.Helpers
{
    public static class SerializationExtensions
    {
        public static T DeepCopy<T>(this T self) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(self));
    }
}