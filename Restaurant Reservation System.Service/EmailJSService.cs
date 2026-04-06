using AutoMapper;
using Restaurant_Reservation_System.Dal.Repositories;
using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service
{
    public class EmailJSService : IEmailJSService
    {
        private readonly IEmailJSRepository _repo;
        private readonly IMapper _mapper;

        public EmailJSService(IEmailJSRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<EmailJSDTO> GetByUserIdAsync(int id)
        {
            var person = await _repo.GetByUserIdAsync(id);
            return _mapper.Map<EmailJSDTO>(person);
        }
        public async Task<bool> UpdateAsync(int id, UpdateEmailJSDTO model)
        {
            var emailJS = await _repo.GetByIdAsync(id);
            if (emailJS == null)
                return false;

            _mapper.Map(model, emailJS);
            await _repo.UpdateAsync(emailJS);
            return true;
        }
    }
}
