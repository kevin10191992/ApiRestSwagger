using ApiRest.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Interface
{
    public interface IPersonas
    {
        Task<List<Persona>> Get();

        Task<List<Persona>> Get(int cedula);

        Task Post(Persona persona);

        Task<string> Put(int cedula, Persona persona);

        Task<string> Delete(int cedula);
    }
}
