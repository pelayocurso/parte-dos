using Ejercicio18.Exceptions;
using parte_dos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace parte_dos.Repositorys
{
    public class PersonaRepository : IPersonaRepository
    {
        public Persona Create(Persona persona)
        {
            return ApplicationDbContext.applicationDbContext.Personas.Add(persona);
        }

        public Persona Read(int id)
        {
            return ApplicationDbContext.applicationDbContext.Personas.Find(id);
        }

        public IQueryable<Persona> All()
        {
            IList<Persona> lista = new List<Persona>(ApplicationDbContext.applicationDbContext.Personas);
            return lista.AsQueryable();
        }

        public void Update(int id, Persona persona)
        {
            if(ApplicationDbContext.applicationDbContext.Personas.Count(e => e.id == persona.id) == 0)
            {
                throw new NoEncontradoException("No he encontrado la entidad.");
            }
            ApplicationDbContext.applicationDbContext.Entry(persona).State = EntityState.Modified;
        }

        public Persona Delete(int id)
        {
            Persona persona = ApplicationDbContext.applicationDbContext.Personas.Find(id);
            if (persona == null)
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Personas.Remove(persona);
            return persona;
        } 
    }
}