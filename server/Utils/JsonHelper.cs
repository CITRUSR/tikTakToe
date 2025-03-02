using System.Text.Json;

namespace server.Utils;

public static class JsonHelper
{
    public static string? GetSocketMessageType(string msg)
    {
        try
        {
            using var document = JsonDocument.Parse(msg);

            foreach (var property in document.RootElement.EnumerateObject())
            {
                if (string.Equals(property.Name, "type", StringComparison.OrdinalIgnoreCase))
                {
                    return property.Value.GetString();
                }
            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Socket message invalid format");
            return null;
        }

        return null;
    }
}
