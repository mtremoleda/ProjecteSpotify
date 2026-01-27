using UI.Spotify.MODELS; // Assegura't que aquest sigui el namespace correcte de la teva classe Canco
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InterficieSpotify
{
    public partial class Songs : Window
    {
        private readonly string apiUrl = "http://localhost:5000/cancons";
        private List<Canco> listaCancions;

        public Songs()
        {
            InitializeComponent();
            Loaded += async (s, e) => await CarregarCancionsAsync();
        }

        private async void CarregarDades_Click(object sender, RoutedEventArgs e)
        {
            await CarregarCancionsAsync();
        }

        private async Task CarregarCancionsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);

<<<<<<< HEAD
                    string url = "http://localhost:5080/cancons";
                    

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode) // Si l'api respon, es llegeix el contingut JSON
=======
                    if (response.IsSuccessStatusCode)
>>>>>>> 5e564e084bbf2838e354eb8df2ff4e843e976b49
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var opciones = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        listaCancions = JsonSerializer.Deserialize<List<Canco>>(json, opciones);
                        dgSongs.ItemsSource = listaCancions;
                        ActualitzarVisibilitatBotonsEliminar();
                    }
                    else
                    {
                        MessageBox.Show("Error al obtenir les dades de la API");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de xarxa: {ex.Message}");
            }
        }

        private void ActualitzarVisibilitatBotonsEliminar()
        {
            foreach (var item in dgSongs.Items)
            {
                var row = dgSongs.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var eliminarButton = FindVisualChild<Button>(row, "btnEliminar");
                    if (eliminarButton != null)
                    {
                        var canco = item as Canco;
                        if (canco != null && canco.Id == Guid.Empty)
                        {
                            eliminarButton.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            eliminarButton.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        private T FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && (child is FrameworkElement element && element.Name == childName))
                {
                    return typedChild;
                }
                else
                {
                    var childOfChild = FindVisualChild<T>(child, childName);
                    if (childOfChild != null) return childOfChild;
                }
            }
            return null;
        }

        private void AfegirCancoDesdeGrid(object sender, RoutedEventArgs e)
        {
            var novaCanco = new Canco
            {
                Id = Guid.Empty,
                Titol = "Nova Cançó",
                Artista = "Nou Artista",
                Album = "Nou Àlbum",
                Durada = 0
            };

            listaCancions.Add(novaCanco);
            dgSongs.Items.Refresh();
            ActualitzarVisibilitatBotonsEliminar();
        }

        private async void GuardarCancoDesdeGrid(object sender, RoutedEventArgs e)
        {
            var boto = sender as Button;
            var canco = boto?.DataContext as Canco;

            if (canco == null) return;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    if (canco.Id == Guid.Empty)
                    {
                        canco.Id = Guid.NewGuid();
                        string json = JsonSerializer.Serialize(canco);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Cançó afegida correctament.");
                            await CarregarCancionsAsync();
                        }
                        else
                        {
                            MessageBox.Show("Error en afegir la cançó.");
                        }
                    }
                    else
                    {
                        string json = JsonSerializer.Serialize(canco);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await client.PutAsync($"{apiUrl}/{canco.Id:D}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Cançó actualitzada correctament.");
                        }
                        else
                        {
                            MessageBox.Show("Error en actualitzar la cançó.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void EliminarCancoDesdeGrid(object sender, RoutedEventArgs e)
        {
            var boto = sender as Button;
            var canco = boto?.DataContext as Canco;

            if (canco == null || canco.Id == Guid.Empty)
            {
                MessageBox.Show("No es pot eliminar una cançó que encara no s'ha desat.");
                return;
            }

            if (MessageBox.Show($"Vols eliminar la cançó '{canco.Titol}'?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        string url = $"{apiUrl}/{canco.Id:D}";
                        var response = await client.DeleteAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Cançó eliminada correctament.");
                            await CarregarCancionsAsync();
                        }
                        else
                        {
                            MessageBox.Show($"Error en eliminar la cançó. Codi: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void Enrrere_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}