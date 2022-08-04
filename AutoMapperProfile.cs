using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_webapi_6.Dtos.Character;

namespace dotnet_webapi_6
{
    public class AutoMapperProfile : Profile
    {
       public AutoMapperProfile()
       {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
       } 
    }
}