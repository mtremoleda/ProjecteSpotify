﻿namespace ApiSpotify.MODELS
{
    public class Usuari
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nom { get; set; }
        public string Contrasenya { get; set; }
        public string Salt { get; set; }

        
        public int RolId { get; set; }

        
        public Rol Rol { get; set; }

    }
}

