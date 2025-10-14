namespace CourseMate.Models.Responses;

public record AuthResult(
    bool Success,
    string? Token,
    string? Role,
    object? User,
    string? Error
)
{
    public static AuthResult WasFail(string error) => new(
        Success: false,
        Token: null,
        Role: null,
        User: null,
        Error: error
    );

    public static AuthResult WasSuccess(string token, string role, object user) => new(
        Success: true,
        Token: token,
        Role: role,
        User: user,
        Error: null
    );
}
