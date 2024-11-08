namespace ProyectoFinalLab.Models
{
    public class Citas
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }

        public string NombreVet {  get; set; }


        public virtual ICollection<Mascota> Mascotas { get; set; }  
        //public virtual ICollection<Veterinario> Veterinario { get; set; }
        public virtual Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public int VeterinarioId { get; set; }
        public virtual Veterinario veterinario { get; set; }







    }
}
