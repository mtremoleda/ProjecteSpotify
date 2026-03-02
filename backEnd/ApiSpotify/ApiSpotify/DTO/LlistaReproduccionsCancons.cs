namespace ApiSpotify.DTO
{
    public class LlistaReproduccioCancoResponse
    {
        public Guid Id { get; set; }
        public string Titol { get; set; }
        public string Artista { get; set; }
        public string Album { get; set; }
        public decimal? Durada { get; set; }
    }
}

