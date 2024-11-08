using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProyectoFinalLab.Areas.Identity.Data;

// Add profile data for application users by adding properties to the IdentityUser class
public class IdentidadUser : IdentityUser
{
    public string PrimerNombre { get; set; }        // ???   HACER RELACIONES Y PONER IMAGENES?
    public string Apellido { get; set; }

}

