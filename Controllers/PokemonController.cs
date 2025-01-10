using Microsoft.AspNetCore.Mvc;
using PokemonAPI.Models;
using PokemonAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pokemon>>> GetAllPokemons()
        {
            var pokemons = await _pokemonService.GetAllPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<Pokemon>>> GetPokemonsByType(string type)
        {
            var pokemons = await _pokemonService.GetPokemonsByTypeAsync(type);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokemon>> GetById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null)
                return NotFound();
            return Ok(pokemon);
        }

        [HttpPost]
        public async Task<ActionResult<Pokemon>> AddPokemon([FromBody] Pokemon pokemon)
        {
            var addedPokemon = await _pokemonService.AddPokemonAsync(pokemon);
            return CreatedAtAction(nameof(GetById), new { id = addedPokemon.Id }, addedPokemon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Pokemon>> UpdatePokemon(int id, [FromBody] Pokemon pokemon)
        {
            var updatedPokemon = await _pokemonService.UpdatePokemonAsync(id, pokemon);
            if (updatedPokemon == null)
                return NotFound();
            return Ok(updatedPokemon);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePokemon(int id)
        {
            var success = await _pokemonService.DeletePokemonAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }
    }
}
