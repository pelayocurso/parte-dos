using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using parte_dos.Models;
using parte_dos.Services;
using Ejercicio18.Exceptions;
using System.Web.Http.Cors;

namespace parte_dos.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class PersonasController : ApiController
    {
        private IPersonaService personaService;

        public PersonasController(IPersonaService personaService)
        {
            this.personaService = personaService;
        }

        // GET: api/Personas
        public IQueryable<Persona> GetPersonas()
        {
            return personaService.getPersonas();
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(int id)
        {
            Persona persona = personaService.getPersona(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(int id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.id)
            {
                return BadRequest();
            }

            try
            {
                personaService.putPersona(id, persona);
            }
            catch (NoEncontradoException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personas
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            persona = personaService.postPersona(persona);

            return CreatedAtRoute("DefaultApi", new { id = persona.id }, persona);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(int id)
        {
            Persona persona;

            try
            {
                persona = personaService.deletePersona(id);
            }
            catch (NoEncontradoException)
            {
                return NotFound();
            }
            return Ok(persona);
        }
    }
}