using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using parte_dos.Models;
using parte_dos.Repositorys;

namespace parte_dos.Services
{
    public class PersonaService : IPersonaService
    {
        private IPersonaRepository personaRepository;
        public PersonaService(IPersonaRepository personaRepository)
        {
            this.personaRepository = personaRepository;
        }

        public Persona deletePersona(int id)
        {
            //Persona persona = personaRepository.Read(id);
            return personaRepository.Delete(id);
        }

        public Persona getPersona(int id)
        {
            return personaRepository.Read(id);
        }

        public IQueryable<Persona> getPersonas()
        {
            IQueryable<Persona> personas;
            personas = personaRepository.All();
            return personas;
        }

        public Persona postPersona(Persona persona)
        {
            return personaRepository.Create(persona);
        }

        public void putPersona(int id, Persona persona)
        {
            personaRepository.Update(id, persona);
        }
    }
}