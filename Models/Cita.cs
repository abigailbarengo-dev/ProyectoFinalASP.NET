namespace ProyectoFinalLab.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }

        public string NombreVet {  get; set; }


        public virtual ICollection<Mascota> Mascotas { get; set; }  
        public virtual ICollection<Veterinario> Veterinario { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }







    }
}
