
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if !WCF
using System.Text.Json.Serialization;
using System.Text.Json;
#endif

namespace fiskaltrust.ifPOS.v2;
#nullable enable

[Newtonsoft.Json.JsonConverter(typeof(PreviousReceiptReferenceConverterNewtonsoft))]
#if !WCF
[JsonConverter(typeof(PreviousReceiptReferenceConverterSystemTextJson))]
#endif
public record cbPreviousReceiptReference
{
    public record Single : cbPreviousReceiptReference { public string Value { get; set; } public Single(string v) => Value = v; };
    public static implicit operator cbPreviousReceiptReference(string v) => string.IsNullOrEmpty(v) ? null : new Single(v);

    public record Group : cbPreviousReceiptReference { public string[] Value { get; set; } public Group(string[] g) => Value = g; };
    public static implicit operator cbPreviousReceiptReference(string[] g) => new Group(g);
    public static implicit operator cbPreviousReceiptReference(List<string> g) => new Group(g.ToArray());

    private cbPreviousReceiptReference() { }

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
    public bool IsSingle => this switch
    {
        Single => true,
        Group => false,
    };

    public bool IsGroup => this switch
    {
        Single => false,
        Group => true,
    };

    public string? SingleValue => this switch
    {
        Single single => single.Value,
        Group => default,
    };

    public string[]? GroupValue => this switch
    {
        Single => default,
        Group group => group.Value,
    };

    public void Match(
        Action<string> single,
        Action<string[]> group)
    {
        Action action = this switch
        {
            Single s => () => single(s.Value),
            Group g => () => group(g.Value)
        };

        action();
    }

    public R Match<R>(
        Func<string, R> single,
        Func<string[], R> group) => this switch
        {
            Single s => single(s.Value),
            Group g => group(g.Value)
        };

    public Task MatchAsync(
        Func<string, Task> single,
        Func<string[], Task> group) => this switch
        {
            Single s => single(s.Value),
            Group g => group(g.Value)
        };

    public Task<R> MatchAsync<R>(
        Func<string, Task<R>> single,
        Func<string[], Task<R>> group) => this switch
        {
            Single s => single(s.Value),
            Group g => group(g.Value)
        };
#pragma warning restore
}

public class PreviousReceiptReferenceConverterNewtonsoft : Newtonsoft.Json.JsonConverter<cbPreviousReceiptReference>
{
    public override cbPreviousReceiptReference ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, cbPreviousReceiptReference existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    =>
        reader.TokenType switch
        {
            Newtonsoft.Json.JsonToken.Null => null,
            Newtonsoft.Json.JsonToken.StartArray => serializer.Deserialize<string[]>(reader),
            Newtonsoft.Json.JsonToken.String => serializer.Deserialize<string>(reader),
            _ => throw new Newtonsoft.Json.JsonSerializationException($"Unexpected token {reader.TokenType} when parsing cbPreviousReceiptReference")
        };


    public override void WriteJson(Newtonsoft.Json.JsonWriter writer, cbPreviousReceiptReference value, Newtonsoft.Json.JsonSerializer serializer)
    =>
        value.Match(
            single => serializer.Serialize(writer, single),
            group => serializer.Serialize(writer, group)
        );

}

#if !WCF
public class PreviousReceiptReferenceConverterSystemTextJson : JsonConverter<cbPreviousReceiptReference>
{
    public override cbPreviousReceiptReference Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    =>
        reader.TokenType switch
        {
            JsonTokenType.Null => null,
            JsonTokenType.StartArray => JsonSerializer.Deserialize<string[]>(ref reader, options),
            JsonTokenType.String => reader.GetString(),
            _ => throw new JsonException($"Unexpected token {reader.TokenType} when parsing cbPreviousReceiptReference")
        };

    public override void Write(Utf8JsonWriter writer, cbPreviousReceiptReference value, JsonSerializerOptions options)
    =>
        value.Match(
            single => JsonSerializer.Serialize(writer, single, options),
            group => JsonSerializer.Serialize(writer, group, options)
        );
}
#endif