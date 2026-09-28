using System.ComponentModel.DataAnnotations;
using Projeto1.Domain.Enums;

namespace Projeto1.Application.Dtos
{
    public record CreateUserDto(
        [Required, StringLength(100, MinimumLength = 3)] string FullName,
        [Required, EmailAddress] string Email,
        [Required, MinLength(6)] string Password,
        [EnumDataType(typeof(UserType))] UserType Type
    );
}
