using Projeto1.Domain.Enums;

namespace Projeto1.Application.Dtos
{
    public record UserResponseDto(
        int Id,
        string FullName,
        string Email,
        UserType Type,
        DateTime CreatedAt
    );
}