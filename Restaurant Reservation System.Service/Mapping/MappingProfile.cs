using AutoMapper;
using Restaurant_Reservation_System.Data.Entities;
using Restaurant_Reservation_System.Service.DTOs.EmailJS;
using Restaurant_Reservation_System.Service.DTOs.Menu;
using Restaurant_Reservation_System.Service.DTOs.Person;
using Restaurant_Reservation_System.Service.DTOs.Reservation;
using Restaurant_Reservation_System.Service.DTOs.Restaurant;
using Restaurant_Reservation_System.Service.DTOs.Role;
using Restaurant_Reservation_System.Service.DTOs.RoleUser;
using Restaurant_Reservation_System.Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_Reservation_System.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Person
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<CreatePersonDTO, Person>();
            CreateMap<UpdatePersonDTO, Person>();

            // User
            CreateMap<User, UserDTO>();
            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Person, opt => opt.Ignore());
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, CustomerDTO>();

            // UserPerson
            CreateMap<User, UserDTO>();
            CreateMap<Person, PersonDTO>();

            // Role 
            CreateMap<Role, RoleDTO>();

            // RoleUser
            CreateMap<RoleUser, RoleUserDTO>().ReverseMap();
            CreateMap<CreateRoleUserDTO, RoleUser>();
            CreateMap<UpdateRoleUserDTO, RoleUser>();

            // Reservation
            //CreateMap<Reservation, ReservationDTO>().ReverseMap();
            //CreateMap<CreateReservationDTO, Reservation>();
            //CreateMap<UpdateReservationDTO, Reservation>();
            // Reservation → DTO
            CreateMap<Reservation, ReservationDTO>();
            CreateMap<CreateReservationDTO, Reservation>();
            CreateMap<UpdateReservationDTO, Reservation>();

            // Create DTO → Entity
            CreateMap<CreateReservationDTO, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StatusId, opt => opt.Ignore());

            // Update DTO → Entity
            CreateMap<UpdateReservationDTO, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // Menu
            CreateMap<Menu, MenuDTO>();
            CreateMap<CreateMenuDTO, Menu>();
            CreateMap<UpdateMenuDTO, Menu>();

            // Restaurant
            CreateMap<Restaurant, RestaurantDTO>().ReverseMap();
            CreateMap<CreateRestaurantDTO, Restaurant>();
            CreateMap<UpdateRestaurantDTO, Restaurant>();

            // EmailJS
            CreateMap<EmailJS, EmailJSDTO>().ReverseMap();
            CreateMap<CreateEmailJSDTO, EmailJS>();
            CreateMap<UpdateEmailJSDTO, EmailJS>();
        }
    }
}
