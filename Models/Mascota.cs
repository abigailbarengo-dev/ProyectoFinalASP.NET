

using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalLab.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public DateOnly? FechaNacimiento { get; set; }
        public TimeOnly? Hora { get; set; }
        public string? Sexo { get; set; }
        public string? Imagen { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
