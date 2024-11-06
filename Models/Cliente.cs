using ProyectoFinalLab.Controllers;

namespace ProyectoFinalLab.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }


        public virtual ICollection<Mascota>? Mascotas { get; set; }

        public virtual ICollection<Cita>? Citas { get; set; }


    }
}
