using System.Text.Json.Serialization;

namespace CodeD.Domain.Abstractions;

public sealed class Error
{
    private readonly Error[]? _errors = null;
    public static readonly Error None = new();

    private Error()
    {
        Code = string.Empty;
        Message = string.Empty;
    }

    public Error(string code, string message, params Error[] errors)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        Code = code;
        Message = message;
        _errors = errors.Length == 0 ? null : errors;
    }

    public string Code { get; }
    public string Message { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyCollection<Error>? Errors => _errors;
}
