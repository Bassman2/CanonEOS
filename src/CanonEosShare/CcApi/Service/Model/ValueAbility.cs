namespace CanonEos.CcApi.Internal;

internal class ValueAbility
{
    [JsonPropertyName("value")]
    public string? Value { get; set; }

    [JsonPropertyName("ability")]
    public List<string>? Ability { get; set; }


}

internal static class ValueAbilityExtention
{
    public static string? Ability(this ValueAbility? valueAbility, out string[]? ability)
    {
        ability = valueAbility?.Ability?.ToArray();
        return valueAbility?.Value;
    }
}