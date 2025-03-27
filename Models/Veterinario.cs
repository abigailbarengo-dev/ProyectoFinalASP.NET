namespace ProyectoFinalLab.Models
{
    public class Veterinario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public string? Imagen { get; set; }

        // Relación uno a muchos: un Veterinario tiene muchas Citas
        public virtual ICollection<Citas> Citas { get; set; }
    }
}
