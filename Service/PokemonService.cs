using MongoDB.Driver;
using PokemonAPI.Models;
using PokemonAPI.Settings;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokemonAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemon> _pokemonCollection;

        public PokemonService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _pokemonCollection = mongoDatabase.GetCollection<Pokemon>(mongoDbSettings.Value.CollectionName);
        }

        public async Task<List<Pokemon>> GetAllPokemonsAsync()
        {
            return await _pokemonCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Pokemon>> GetPokemonsByTypeAsync(string type)
        {
            return await _pokemonCollection.Find(p => p.Type.ToLower() == type.ToLower()).ToListAsync();
        }

        public async Task<Pokemon> AddPokemonAsync(Pokemon pokemon)
        {
            await _pokemonCollection.InsertOneAsync(pokemon);
            return pokemon;
        }

        public async Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon)
        {
            var result = await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, pokemon);
            return result.IsAcknowledged ? pokemon : null;
        }

        public async Task<bool> DeletePokemonAsync(int id)
        {
            var result = await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
