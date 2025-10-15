using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using UI.Spotify.MODELS;

namespace InterficieSpotify
{
    public partial class Songs : Window
    {
        public Songs()
        {
            InitializeComponent();
        }

        // 🔹 Evento del botón "CarregarDades"
        private async void CarregarDades_Click(object sender, RoutedEventArgs e)
        {
            await CargarCancionesAsync();
        }

        // 🔹 Método para llamar a la API y llenar el DataGrid
        private async Task CargarCancionesAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = "  http://localhost:5000/cancons"; // Tu API real
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var canciones = JsonSerializer.Deserialize<List<Canco>>(json);

                        dgSongs.ItemsSource = canciones; // Llenamos el DataGrid
                    }
                    else
                    {
                        MessageBox.Show("Error al obtener los datos de la API");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error de red: {ex.Message}");
            }
        }

        // 🔹 Botón "Enrrere" para volver al MainWindow
        private void Enrrere_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }
    }
}
