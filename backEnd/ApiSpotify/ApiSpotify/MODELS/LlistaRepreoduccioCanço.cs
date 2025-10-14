﻿namespace ApiSpotify.MODELS
{
    public class LlistaReproduccioCanco
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdCanco { get; set; }
        public Canco Canco { get; set; }
        public Guid IdLlista { get; set; }
        public LlistaReproduccio LlistaReproduccio { get; set; }
    }
}
