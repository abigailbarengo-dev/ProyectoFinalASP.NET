namespace ProyectoFinalLab.Models
{
    public class CitaVeterinario
    {

        public int VeterinarioId { get; set; }
        public int CitaId { get; set; }
        public Veterinario? Veterinario { get; set; }
        public Cita? Cita { get; set; }


    }
}
