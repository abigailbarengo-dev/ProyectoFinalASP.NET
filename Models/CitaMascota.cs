namespace ProyectoFinalLab.Models
{
    public class CitaMascota
    {
        public int MascotaId { get; set; }
        public int CitaId { get; set; }
        public Mascota? Mascota { get; set; }
        public Cita? Cita { get; set; }


    }
}
