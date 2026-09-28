using Projeto1.Application.Dtos;
using Projeto1.Domain.Entities;
using Projeto1.Domain.Enums;
using Projeto1.Domain.Interfaces;

namespace Projeto1.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<List<UserResponseDto>> GetUsersAsync(UserType? type = null)
        {
            var users = await _repo.GetUsersAsync(type);
            return users.Select(ToResponse).ToList();
        }
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user is null ? null : ToResponse(user);
        }
        public async Task<UserResponseDto?> CreateUserAsync(CreateUserDto dto)
        {
            if (await _repo.ExistsByEmailAsync(dto.Email))
            {
                throw new InvalidOperationException($"Email '{dto.Email}' is already in use.");                
            }
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Type = dto.Type
            };
            await _repo.AddAsync(user);
            return ToResponse(user);
        }
        public async Task UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user is null)
            {
                throw new KeyNotFoundException($"User {id} not found.");
            }
            user.FullName = dto.FullName;
            user.Type = dto.Type;
            await _repo.UpdateAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user is null)
            {
                throw new KeyNotFoundException($"User {id} not found.");
            }
            await _repo.DeleteAsync(user);
        }
        private static UserResponseDto ToResponse(User user) =>
            new(user.Id, user.FullName, user.Email, user.Type, user.CreatedAt);
    }
}
