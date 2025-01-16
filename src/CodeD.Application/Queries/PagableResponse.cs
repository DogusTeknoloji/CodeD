namespace CodeD.Application.Queries;

public record PagableResponse<T>(int TotalRowCaount, IEnumerable<T> Value);
