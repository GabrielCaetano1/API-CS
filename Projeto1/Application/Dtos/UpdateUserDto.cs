using System.ComponentModel.DataAnnotations;
using Projeto1.Domain.Enums;

namespace Projeto1.Application.Dtos
{
    public record UpdateUserDto(
        [Required, StringLength(100, MinimumLength = 3)] string FullName,
        [EnumDataType(typeof(UserType))] UserType Type
    );
}
