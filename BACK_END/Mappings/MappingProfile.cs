using AutoMapper;
using MY_CALS_BACKEND.Dto;
using MY_CALS_BACKEND.Dto.UserAccount;
using MY_CALS_BACKEND.Dto.Meals;
using MY_CALS_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_CALS_BACKEND.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAccount, UserRegisterDTO>(); //Map from Model Object to ModelDTO Object
            CreateMap<UserRegisterDTO, UserAccount>();

            CreateMap<UserAccount, UserForDisplayDTO>();
            CreateMap<UserForDisplayDTO, UserAccount>();

            CreateMap<Meal, MealForAddDTO>();
            CreateMap<MealForAddDTO, Meal>();

            CreateMap<Meal, MealForDisplayDTO>();
            CreateMap<MealForDisplayDTO, Meal>();

            CreateMap<Meal, MealForEditDTO>();
            CreateMap<MealForEditDTO, Meal>();
        }
    }
}
