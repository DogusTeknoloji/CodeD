namespace CodeD.Domain.Abstractions;

public sealed class Error
{
    private readonly Error[] _errors;
    public static readonly Error None = new();

    private Error()
    {
        Code = string.Empty;
        Message = string.Empty;
        _errors = [];
    }

    public Error(string code, string message, params Error[] errors)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        Code = code;
        Message = message;
        _errors = errors;
    }

    public string Code { get; }
    public string Message { get; }

    public IReadOnlyCollection<Error> Errors => _errors;
}
