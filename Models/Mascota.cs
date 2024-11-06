
namespace ProyectoFinalLab.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public DateTime? FechaNacimiento { get; set; }


        public virtual ICollection<Cliente> Clientes { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }

    }
}
