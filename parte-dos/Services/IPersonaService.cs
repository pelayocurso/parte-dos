using parte_dos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parte_dos.Services
{
    public interface IPersonaService
    {
        Persona getPersona(int id);
        IQueryable<Persona> getPersonas();
        Persona postPersona(Persona persona);
        void putPersona(int id, Persona persona);
        Persona deletePersona(int id);
    }
}
