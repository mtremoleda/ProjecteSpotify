using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace InterficieSpotify
{
    public partial class AssignPermisos : Window
    {
        private HttpClient _httpClient = new HttpClient();
        private List<Rol> rols;
        private List<PermisoViewModel> permisos;

        public AssignPermisos()
        {
            InitializeComponent();

            _httpClient.BaseAddress = new Uri("http://localhost:5080/");

            CarregarRols();
        }

        private void CarregarRols()
        {
            
            rols = new List<Rol>
            {
                new Rol { Id = Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1"), Nom = "Administrador" },
                new Rol { Id = Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932"), Nom = "Editor" },
                new Rol { Id = Guid.Parse("c8d2a1af-7e19-4784-a4e1-e216a7c43c72"), Nom = "Artista" },
                new Rol { Id = Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112"), Nom = "Usuari" },
                new Rol { Id = Guid.Parse("ec7f93e3-2da3-45fd-87f8-07b382e92051"), Nom = "Moderador" }
            };

            cbRoles.ItemsSource = rols;
            cbRoles.DisplayMemberPath = "Nom";
            cbRoles.SelectedValuePath = "Id";
            cbRoles.SelectionChanged += CbRoles_SelectionChanged;
        }

        private void CbRoles_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbRoles.SelectedValue == null) return;
            Guid rolId = (Guid)cbRoles.SelectedValue;
            CarregarPermisos(rolId);
        }

        private void CarregarPermisos(Guid rolId)
        {
            permisos = new List<PermisoViewModel>
            {
                // ADMINISTRADOR
                new PermisoViewModel("f81d4fae-7dec-11d0-a765-00a0c91e6bf6", "Crear usuaris", rolId == Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1")),
                new PermisoViewModel("f81d4faf-7dec-11d0-a765-00a0c91e6bf6", "Editar usuaris", rolId == Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1")),
                new PermisoViewModel("f81d4fb0-7dec-11d0-a765-00a0c91e6bf6", "Eliminar usuaris", rolId == Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1")),
                new PermisoViewModel("f81d4fb1-7dec-11d0-a765-00a0c91e6bf6", "Assignar rols", rolId == Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1")),
                new PermisoViewModel("f81d4fb2-7dec-11d0-a765-00a0c91e6bf6", "Veure estadístiques", rolId == Guid.Parse("a7c1e3b2-8e47-4a91-b7f1-2241ea0c1fc1")),

                // EDITOR
                new PermisoViewModel("f81d4fb3-7dec-11d0-a765-00a0c91e6bf6", "Crear cançons", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),
                new PermisoViewModel("f81d4fb4-7dec-11d0-a765-00a0c91e6bf6", "Editar cançons", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),
                new PermisoViewModel("f81d4fb5-7dec-11d0-a765-00a0c91e6bf6", "Eliminar cançons", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),
                new PermisoViewModel("f81d4fb6-7dec-11d0-a765-00a0c91e6bf6", "Crear artistes", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),
                new PermisoViewModel("f81d4fb7-7dec-11d0-a765-00a0c91e6bf6", "Editar artistes", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),
                new PermisoViewModel("f81d4fb8-7dec-11d0-a765-00a0c91e6bf6", "Gestionar àlbums", rolId == Guid.Parse("b3f9c12e-5cbe-4389-9c13-4d7e3f76a932")),

                // ARTISTA
                new PermisoViewModel("f81d4fb9-7dec-11d0-a765-00a0c91e6bf6", "Crear cançons pròpies", rolId == Guid.Parse("c8d2a1af-7e19-4784-a4e1-e216a7c43c72")),
                new PermisoViewModel("f81d4fba-7dec-11d0-a765-00a0c91e6bf6", "Editar cançons pròpies", rolId == Guid.Parse("c8d2a1af-7e19-4784-a4e1-e216a7c43c72")),
                new PermisoViewModel("f81d4fbb-7dec-11d0-a765-00a0c91e6bf6", "Gestionar àlbums propis", rolId == Guid.Parse("c8d2a1af-7e19-4784-a4e1-e216a7c43c72")),
                new PermisoViewModel("f81d4fbc-7dec-11d0-a765-00a0c91e6bf6", "Veure estadístiques pròpies", rolId == Guid.Parse("c8d2a1af-7e19-4784-a4e1-e216a7c43c72")),

                // USUARI
                new PermisoViewModel("f81d4fbd-7dec-11d0-a765-00a0c91e6bf6", "Escoltar cançons", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),
                new PermisoViewModel("f81d4fbe-7dec-11d0-a765-00a0c91e6bf6", "Crear playlists", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),
                new PermisoViewModel("f81d4fbf-7dec-11d0-a765-00a0c91e6bf6", "Editar playlists", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),
                new PermisoViewModel("f81d4fc0-7dec-11d0-a765-00a0c91e6bf6", "Valorar cançons", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),
                new PermisoViewModel("f81d4fc1-7dec-11d0-a765-00a0c91e6bf6", "Seguir artistes", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),
                new PermisoViewModel("f81d4fc2-7dec-11d0-a765-00a0c91e6bf6", "Comentar cançons", rolId == Guid.Parse("d21b84c4-9c55-441a-8e8f-9a7cd2f6b112")),

                // MODERADOR
                new PermisoViewModel("f81d4fc3-7dec-11d0-a765-00a0c91e6bf6", "Eliminar comentaris", rolId == Guid.Parse("ec7f93e3-2da3-45fd-87f8-07b382e92051")),
                new PermisoViewModel("f81d4fc4-7dec-11d0-a765-00a0c91e6bf6", "Suspensió temporal", rolId == Guid.Parse("ec7f93e3-2da3-45fd-87f8-07b382e92051")),
            };

            dgPermisos.ItemsSource = permisos;
        }

        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cbRoles.SelectedValue == null) return;

            Guid rolId = (Guid)cbRoles.SelectedValue;

            try
            {
                // Obtenim permisos actuals del rol
                List<RolPermisos> rolPermisosActuals = await _httpClient
                    .GetFromJsonAsync<List<RolPermisos>>($"rols/{rolId}/permisos");

                foreach (var p in permisos)
                {
                    bool estavaAssignat = rolPermisosActuals.Exists(rp => rp.PermisosId == p.Id);

                    if (p.Assignat && !estavaAssignat)
                    {
                        // Assigna permís
                        var request = new RolPermisosRequest
                        {
                            RolId = rolId,
                            PermisosId = p.Id
                        };
                        await _httpClient.PostAsJsonAsync($"rols/{rolId}/permisos", request);
                    }
                    else if (!p.Assignat && estavaAssignat)
                    {
                        // Desassigna permís
                        var rp = rolPermisosActuals.Find(r => r.PermisosId == p.Id);
                        if (rp != null)
                        {
                            await _httpClient.DeleteAsync($"rolpermisos/{rp.Id}");
                        }
                    }
                }

                MessageBox.Show("Permisos actualitzats correctament!");
                await CarregarPermisosAsync(rolId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error actualitzant permisos: " + ex.Message);
            }
        }

        private async Task CarregarPermisosAsync(Guid rolId)
        {
            try
            {
                List<RolPermisos> rolPermisosActuals = await _httpClient
                    .GetFromJsonAsync<List<RolPermisos>>($"rols/{rolId}/permisos");

                permisos.ForEach(p =>
                {
                    p.Assignat = rolPermisosActuals.Exists(rp => rp.PermisosId == p.Id);
                });

                dgPermisos.ItemsSource = null;
                dgPermisos.ItemsSource = permisos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error carregant permisos: " + ex.Message);
            }
        }
    }


    //AIXO ANIRA EN UNA CARPETA A PART:
    public class Rol
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
    }

    public class PermisoViewModel
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public bool Assignat { get; set; }

        public PermisoViewModel() { }

        public PermisoViewModel(string id, string nom, bool assignat)
        {
            Id = Guid.Parse(id);
            Nom = nom;
            Assignat = assignat;
        }
    }

    public class RolPermisos
    {
        public Guid Id { get; set; }
        public Guid RolId { get; set; }
        public Guid PermisosId { get; set; }
    }

    public class RolPermisosRequest
    {
        public Guid RolId { get; set; }
        public Guid PermisosId { get; set; }
    }
}
