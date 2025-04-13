namespace Domain.Models.Wrappers;

public enum ErrorTypeCode
{
    None,
    NotFound,
    EntityConflict,
    ValidationError,
    NotAuthorized
}