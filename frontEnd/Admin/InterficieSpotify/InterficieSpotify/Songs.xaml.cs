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
            InitializeComponent(); // Elements visulas defints en Songs.xaml (interficie de songs)
            Loaded += async (s, e) => await CargarCancionesAsync();
        }

        private async void CarregarDades_Click(object sender, RoutedEventArgs e)
        {
            await CargarCancionesAsync();
        }

        private async Task CargarCancionesAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient()) // Per fer la paticio GET
                {

                    string url = "http://localhost:5000/cancons";
                    

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode) // Si l'api respon, es llegeix el contingut JSON
                    {
                        string json = await response.Content.ReadAsStringAsync(); // Llegir el contingut de l'api en string

                        var opciones = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true //ignora les majuscules/minuscules a l'hora d'emaprellar-lo amb la calsse.
                                                                        
                        };
                        var canciones = JsonSerializer.Deserialize<List<Canco>>(json, opciones); // Pasa al JSON a una llista d'objectes


                        dgSongs.ItemsSource = canciones; // Aqui emplenem el DataGrid i ho mostra en el recuadra 
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

        
        private void Enrrere_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void dgSongs_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
