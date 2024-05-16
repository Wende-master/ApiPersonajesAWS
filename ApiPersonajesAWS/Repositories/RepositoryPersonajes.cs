using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonajesAWS.Repositories
{
    public class RepositoryPersonajes
    {
        private TelevisionContext context;

        public RepositoryPersonajes(TelevisionContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes
                .ToListAsync();
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await this.context.Personajes
                .Where(x => x.IdPersonaje == id).FirstOrDefaultAsync();
        }

        public async Task CreatePersonajeAsync(Personaje personaje)
        {
            personaje.IdPersonaje = await GetMaxIdAsync();
            await this.context.Personajes.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int id)
        {
            Personaje personaje = 
                await this.FindPersonajeAsync(id);
            this.context.Personajes.Remove(personaje);
            await this.context.SaveChangesAsync();
        }

        private async Task<int> GetMaxIdAsync()
        {
            return await this.context.Personajes
                .MaxAsync(z =>  z.IdPersonaje) + 1;

            //if (this.context.Personajes.Count() == 0)
            //{
            //    return 1;
            //}
            //else
            //{
            //    return await this.context.Personajes.MaxAsync(x => x.IdPersonaje) + 1;
            //}
        }
    }
}
