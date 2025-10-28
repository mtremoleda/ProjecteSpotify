
using System.Collections.Generic;

public class Rol
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Descripcio { get; set; }

    public ICollection<User> Users { get; set; }

    public ICollection<RolPermiso> RolPermisos { get; set; }
}