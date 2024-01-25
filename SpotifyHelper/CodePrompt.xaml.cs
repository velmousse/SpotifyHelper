using Newtonsoft.Json;
using SpotifyHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpotifyHelper
{
    public partial class CodePrompt : Window
    {
        public CodePrompt()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = RegisterCredentials(ClientIdTxt.Text, ClientSecretTxt.Text);
            if (result.Result != null) 
                GenerateMainPage(result.Result);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var result = RegisterCredentials(ClientIdTxt.Text, ClientSecretTxt.Text);
                if (result.Result != null)
                    GenerateMainPage(result.Result);
            }
        }

        private async Task<AccessTokenResponse?> RegisterCredentials(string clientId, string clientSecret)
        {

            if (ClientIdTxt.Text.Equals(string.Empty) || ClientSecretTxt.Text.Equals(string.Empty))
            {
                ErrorTxt.Visibility = Visibility.Visible;
                return null;
            }

            using (HttpClient client = new HttpClient())
            {
                var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                });

                HttpResponseMessage response = await client.PostAsync("https://accounts.spotify.com/api/token", requestData);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    #pragma warning disable CS8600
                    AccessTokenResponse accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseBody);
                    return accessTokenResponse;
                }
                else
                {
                    throw new HttpRequestException($"An error occurred while processing your request : {response.StatusCode}");
                }
            }
        }

        private void GenerateMainPage(AccessTokenResponse accessTokenResponse)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
