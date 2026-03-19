using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task<PersonDTO> GetByIdAsync(int id);
        Task<PersonDTO> CreateAsync(CreatePersonDTO model);
        Task<bool> UpdateAsync(int id, UpdatePersonDTO model);
        Task<bool> DeleteAsync(int id);
    }
}
