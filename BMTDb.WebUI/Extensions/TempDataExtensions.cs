using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace BMTDb.WebUI.Extensions
{
    public static class TempDataExtensions
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value, _jsonOptions);
        }

        public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            return !tempData.TryGetValue(key, out var value) || value is not string json
                ? null
                : JsonSerializer.Deserialize<T>(json, _jsonOptions);
        }
    }
}