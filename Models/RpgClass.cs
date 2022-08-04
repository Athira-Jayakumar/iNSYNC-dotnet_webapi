using System.Text.Json.Serialization;

namespace dotnet_webapi_6.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight  = 1,
        Mage = 2,
        Cleric = 3
    }
}