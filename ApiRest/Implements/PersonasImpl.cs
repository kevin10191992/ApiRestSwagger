using ApiRest.Context;
using ApiRest.Interface;
using ApiRest.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Implements
{
    public class PersonasImpl : IPersonas
    {
        private Contexto _contexto;

        public PersonasImpl(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Persona>> Get(int cedula) => await _contexto.Persona.Where(a => a.Cedula.Equals(cedula.ToString())).ToListAsync();

        public async Task<List<Persona>> Get() => await _contexto.Persona.ToListAsync();

        public async Task Post(Persona persona)
        {
            await _contexto.AddAsync(persona);
            await _contexto.SaveChangesAsync();
        }

        public async Task<string> Put(int cedula, Persona persona)
        {
            Persona cliente = await _contexto.Persona.Where(a => a.Cedula.Equals(cedula.ToString())).FirstOrDefaultAsync();
            if (cliente != null)
            {
                if (cliente.Cedula != persona.Cedula) { return "03"; }

                cliente = persona;
                await _contexto.SaveChangesAsync();
                return "01";
            }
            else
            {
                return "02";

            }
        }

        public async Task<string> Delete(int cedula)
        {
            Persona persona = await _contexto.Persona.Where(a => a.Cedula.Equals(cedula.ToString())).FirstOrDefaultAsync();

            if (persona == null)
            {
                return "02";
            }
            else
            {
                _contexto.Remove(persona);
                await _contexto.SaveChangesAsync();
                return "01";
            }
        }
    }
}
