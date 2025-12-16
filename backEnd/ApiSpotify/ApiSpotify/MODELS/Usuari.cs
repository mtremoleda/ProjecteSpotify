namespace ApiSpotify.MODELS
{

    public class Usuari
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nom { get; set; }
        public string Contrasenya { get; set; }
        public string Salt { get; set; }
    

    public static Usuari BuildUsuari(Guid id, string nom, string hashedPassword, string salt)
        {
            return new Usuari
            {
                Id = id,
                Nom = nom,
                Contrasenya = hashedPassword,
                Salt = salt
            };
        }
    }
}


