namespace ApiSpotify.MODELS;

using System.Collections.Generic;

public class Rol
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Codi { get; set; }
    public string Nom { get; set; }
    public string Descripcio { get; set; }  

    
}