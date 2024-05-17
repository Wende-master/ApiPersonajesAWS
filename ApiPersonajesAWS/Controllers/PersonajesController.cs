using ApiPersonajesAWS.Models;
using ApiPersonajesAWS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPersonajesAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Personaje>>> Personajes()
        {
            List<Personaje> personajes =
                await this.repo.GetPersonajesAsync();
            return personajes;
        }

        [HttpGet("{idpersonaje}")]
        //[Route("[action]")]
        public async Task<ActionResult<Personaje>> FindPersonaje(int idpersonaje)
        {
            return await this.repo.FindPersonajeAsync(idpersonaje);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePersonaje(Personaje personaje)
        {
            await this.repo.UpdatePersonajeAsync(personaje);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CrearPersonaje(Personaje personaje)
        {
            await this.repo.CreatePersonajeAsync(personaje);
            return Ok();
        }

        [HttpDelete("{idpersonaje}")]
        public async Task<ActionResult> DeletePersonaje(int idpersonaje)
        {
            await this.repo.DeletePersonajeAsync(idpersonaje);
            return Ok();
        }
    }
}
