using PokemonAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonAPI.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> GetAllPokemonsAsync();
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<List<Pokemon>> GetPokemonsByTypeAsync(string type);
        Task<Pokemon> AddPokemonAsync(Pokemon pokemon);
        Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon);
        Task<bool> DeletePokemonAsync(int id);
    }
}