using parte_dos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parte_dos.Repositorys
{
    public interface IPersonaRepository
    {
        Persona Read(int id);
        IQueryable<Persona> All();
        Persona Create(Persona persona);
        void Update(int id, Persona persona);
        Persona Delete(int id);
    }
}
