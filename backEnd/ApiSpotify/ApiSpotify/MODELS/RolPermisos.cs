using ApiSpotify.MODELS;

public class RolPermiso
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RolId { get; set; }
    public Guid PermisoId { get; set; }
}