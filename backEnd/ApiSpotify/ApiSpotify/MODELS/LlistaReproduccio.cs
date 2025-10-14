namespace ApiSpotify.MODELS
{
    public class LlistaReproduccio
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdUsuari { get; set; }
        public Usuari Usuari { get; set; }
        public string Nom { get; set; }

        public ICollection<LlistaReproduccioCanco> Cancons { get; set; }
    }
}
