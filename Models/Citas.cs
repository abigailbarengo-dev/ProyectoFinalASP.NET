using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalLab.Models
{
    public class Citas
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }

        [Display(Name = "Mascota")]
        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Veterinario")]
        public int VeterinarioId { get; set; }
        public virtual Veterinario Veterinario { get; set; }







    }
}
