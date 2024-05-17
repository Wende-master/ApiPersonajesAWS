using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ApiPersonajesAWS.Repositories
{
    #region PROCEDURES

    //    DELIMITER //
    //    CREATE PROCEDURE SP_UPDATE_PERSONAJE(
    //        IN p_IdPersonaje INT,
    //        IN p_Nombre VARCHAR(255),
    //    IN p_Imagen VARCHAR(255)
    //)
    //BEGIN
    //    UPDATE PERSONAJES
    //    SET PERSONAJE = p_Nombre,
    //        IMAGEN = p_Imagen
    //    WHERE IDPERSONAJE = p_IdPersonaje;
    //END //
    //DELIMITER;

    #endregion
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

        public async Task UpdatePersonajeAsync(Personaje personaje)
        {
            string sql = "CALL SP_UPDATE_PERSONAJE(@p_IdPersonaje, @p_Nombre, @p_Imagen)";

            var parameters = new[]
            {
            new MySqlParameter("@p_IdPersonaje", personaje.IdPersonaje),
            new MySqlParameter("@p_Nombre", personaje.Nombre),
            new MySqlParameter("@p_Imagen", personaje.Imagen)
        };

            await context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        private async Task<int> GetMaxIdAsync()
        {
            return await this.context.Personajes
                .MaxAsync(z => z.IdPersonaje) + 1;

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
