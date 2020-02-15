using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ApiRest.Model
{
    /// <summary>
    /// Clase persona
    /// </summary>
    public class Persona
    {
        /// <summary>
        /// AutoIncrementral
        /// </summary>
        [JsonIgnore]
        public int PersonaId { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        [StringLength(1)]
        public string Genero { get; set; }
        [Required]
        public int Edad { get; set; }
    }
}
