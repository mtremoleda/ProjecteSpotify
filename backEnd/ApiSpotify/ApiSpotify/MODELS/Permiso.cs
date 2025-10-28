namespace ApiSpotify.MODELS;
using System.Collections.Generic;

public class Permiso
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Descripcio { get; set; }

    
    public ICollection<RolPermiso> RolPermisos { get; set; }
}