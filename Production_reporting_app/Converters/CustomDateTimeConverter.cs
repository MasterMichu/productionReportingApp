using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
public class CustomDateTimeConverter : JsonConverter<DateTime> 
{
    private const string InputDateFormat = "yyyy-MM-ddTHH:mm:ss";
    private const string DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) 
        {
            throw new JsonException(); 
        }
        string dateString = reader.GetString();
        if (DateTime.TryParseExact(dateString, InputDateFormat, null, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTime date))
        { 
            
            return date;
            
        } 
        throw new JsonException($"Unable to parse DateTime string: {dateString}"); 
    } public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) 
    { 
        writer.WriteStringValue(value.ToString(DateFormat)); 
    } 
}