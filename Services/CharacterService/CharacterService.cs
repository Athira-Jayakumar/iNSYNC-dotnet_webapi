using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_webapi_6.Data;
using dotnet_webapi_6.Dtos.Character;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi_6.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
       
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper ,DataContext context)
        {
          _mapper = mapper;
           _context = context;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
          var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
           Character character = _mapper.Map<Character>(newCharacter);
           
           _context.characters.Add(character);
           await _context.SaveChangesAsync();
           serviceResponse.Data = await _context.characters
           .Select(c => _mapper.Map<GetCharacterDto>(c))
           .ToListAsync();
           return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
          ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

           try
           {
               Character character = await _context.characters.FirstAsync(c => c.Id == id);
               _context.characters.Remove(character);
               await _context.SaveChangesAsync();
               response.Data = _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
           
           }
           catch(Exception ex)
           {
            response.Success = false;
            response.Message = ex.Message;
           }
           return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
           var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.characters.ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
             return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbcharacter =await _context.characters.FirstAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
           ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto> ();
           try{
           var character = await _context.characters
            .FirstAsync(c => c.Id == updatedCharacter.Id);
            //_mapper.Map(updatedCharacter, character);

             character.Name = updatedCharacter.Name;
             character.HitPoints = updatedCharacter.HitPoints;
             character.Strength = updatedCharacter.Strength;
             character.Intelligence = updatedCharacter.Intelligence;
             character.Class = updatedCharacter.Class;

             await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterDto>(character);
           }
           catch(Exception ex)
           {
            response.Success = false;
            response.Message = ex.Message;
           }
           return response;
        }
    }
}