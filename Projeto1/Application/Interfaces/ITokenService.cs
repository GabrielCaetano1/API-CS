using Projeto1.Domain.Entities;

namespace Projeto1.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
