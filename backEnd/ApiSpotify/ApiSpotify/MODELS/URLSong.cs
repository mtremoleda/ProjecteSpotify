namespace ApiSpotify.MODELS
{
    public class URLSong
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SongId { get; set; }
        public string Url { get; set; }
    }

}
