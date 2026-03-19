using AutoMapper;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.Person;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // person
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<CreatePersonDTO, Person>();
            CreateMap<UpdatePersonDTO, Person>();
        }
    }
}
