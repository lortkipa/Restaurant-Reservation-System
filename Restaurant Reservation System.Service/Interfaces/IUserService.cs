using Restaurant_Reservation_System.Service.DTOs;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task<UserDTO> GetByUsernameAsync(string username);
        Task<AuthResponseDTO> RegisterAsync(RegisterUserDTO model);
        Task<AuthResponseDTO> LoginAsync(LoginUserDTO model);
        Task<bool> UpdateAsync(int id, UpdateUserDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
