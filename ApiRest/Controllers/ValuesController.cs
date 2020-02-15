using System;
using System.Threading.Tasks;
using ApiRest.Interface;
using ApiRest.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ApiRest.Controllers
{
    /// <summary>
    /// Controlador 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {


        private readonly IPersonas _Persona;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="persona"></param>
        public ValuesController(IPersonas persona)
        {
            _Persona = persona;
        }




        /// <summary>
        /// Retorna las personas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(await _Persona.Get());
        }

        /// <summary>
        /// Devuelve una persona
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        [HttpGet("{cedula}")]
        public async Task<IActionResult> Get(int cedula)
        {
            if (cedula <= 0)
            {
                return NotFound(new { Codigo = "02", Descripcion = "Persona no existe" });
            }

            return new JsonResult(await _Persona.Get(cedula));
        }



        /// <summary>
        /// Crea una persona
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Persona
        ///     {
        ///       "Nombres": "kevin",
        ///       "Apellidos": "Perez",
        ///       "Cedula": "123456789",
        ///       "Genero": "M",
        ///       "Edad": 27
        ///     }
        ///
        /// </remarks>
        /// <param name="persona"></param>
        /// <returns>Nueva persona creada</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Persona persona)
        {
            JObject result = new JObject();
            if (persona == null)
            {
                result.Add("Codigo", "02");
                result.Add("Descripcion", "Bad Request");
                return BadRequest(result);
            }
            if (string.IsNullOrEmpty(persona.Nombres) || string.IsNullOrEmpty(persona.Apellidos) || string.IsNullOrEmpty(persona.Cedula))
            {
                result.Add("Codigo", "02");
                result.Add("Descripcion", "Se necesita un minimo de datos para crear a la persona");
                return BadRequest(result);
            }
            else
            {
                try
                {

                    await _Persona.Post(persona);
                    result.Add("Codigo", "01");
                    result.Add("Descripcion", "Nueva Persona Creada");
                }
                catch (Exception ex)
                {
                    result = new JObject();
                    result.Add("Codigo", "02");
                    result.Add("Descripcion", "No se pudo crear a la persona");

                }

            }


            return new JsonResult(result);
        }


        /// <summary>
        /// Permite actualizar a una persona
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Persona
        ///     {
        ///         "Nombres": "kevin Ivanot",
        ///         "Apellidos": "Perez Rondon",
        ///         "Cedula": "123456789",
        ///         "Genero": "M",
        ///         "Edad": 32
        ///     }
        ///
        /// </remarks>
        /// <param name="cedula"></param>
        /// <param name="persona"></param>
        /// <returns></returns>
        [HttpPut("{cedula}")]
        public async Task<IActionResult> Put(int cedula, [FromBody] Persona persona)
        {
            JObject result = new JObject();

            if (cedula <= 0)
            {
                return BadRequest(new { Codigo = "02", Descripcion = "Persona no existe" });
            }

            if (persona == null)
            {
                result.Add("Codigo", "02");
                result.Add("Descripcion", "El objeto persona es necesario");
                return BadRequest(result);
            }

            if (string.IsNullOrEmpty(persona.Nombres) || string.IsNullOrEmpty(persona.Apellidos) || persona.Edad <= 0 || string.IsNullOrEmpty(persona.Genero))
            {
                result.Add("Codigo", "02");
                result.Add("Descripcion", "Se necesita un minimo de datos para actualizar a la persona");
                return BadRequest(result);
            }
            else
            {


                try
                {
                    string Codigo = await _Persona.Put(cedula, persona);

                    switch (Codigo)
                    {
                        case "01":
                            result.Add("Codigo", Codigo);
                            result.Add("Descripcion", "Persona Actualizada.");
                            break;
                        case "02":
                            result.Add("Codigo", Codigo);
                            result.Add("Descripcion", "Persona No existe.");
                            break;
                        case "03":
                            return BadRequest(new { Codigo, Descripcion = "Una Persona no puede cambiar de numero de cedula" });
                    }



                }
                catch (Exception ex)
                {
                    result = new JObject();
                    result.Add("Codigo", "02");
                    result.Add("Descripcion", "No se pudo Actualizar a la persona");

                }
            }


            return new JsonResult(result);
        }

        /// <summary>
        /// Elimina a una persona
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Delete(int cedula)
        {
            JObject result = new JObject();

            if (cedula <= 0)
            {
                return BadRequest(new { Codigo = "02", Descripcion = "Persona no existe" });
            }

            try
            {
                string Codigo = await _Persona.Delete(cedula);
                switch (Codigo)
                {
                    case "01":
                        result.Add("Codigo", "01");
                        result.Add("Descripcion", "Persona Eliminada");
                        break;
                    case "02":
                        result.Add("Codigo", "02");
                        result.Add("Descripcion", $"La persona con id: {cedula} no existe");
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Add("Codigo", "02");
                result.Add("Descripcion", "No se pudo eliminar a la Persona");
            }




            return new JsonResult(result);
        }
    }
}
