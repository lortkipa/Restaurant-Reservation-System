using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repo;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var persons = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDTO>>(persons);
        }
        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            return person == null ? null : _mapper.Map<PersonDTO>(person);
        }
        public async Task<PersonDTO> CreateAsync(CreatePersonDTO model)
        {
            var person = _mapper.Map<Person>(model);

            await _repo.AddAsync(person);
            return _mapper.Map<PersonDTO>(person);
        }
        public async Task<bool> UpdateAsync(int id, UpdatePersonDTO model)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null)
                return false;

            person = _mapper.Map<Person>(person);
            await _repo.UpdateAsync(person);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _repo.GetByIdAsync(id);
            if (person == null)
                return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
