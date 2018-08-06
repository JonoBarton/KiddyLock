using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.Models;

namespace KiddyLock.UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> _osUsers;
        private List<User> _users;
        private User _selectedUser;
        private AuthResponse _authResponse;
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string ClientId = "133086194188-h89cbj49flgc1jiaddt1d7ep4sbpupt1.apps.googleusercontent.com";
        public MainWindow()
        {
            InitializeComponent();
            Login.Loaded += Landing_Loaded;
        }

        private async void Landing_Loaded(object sender, RoutedEventArgs e)
        {
            _authResponse = await DoLogin();
            if (!_authResponse.IsAuthenticated)
            {
                MessageBox.Show("Unable to authenticate");
            }
            // _authResponse.UserInfo.Picture;
            this.Hide();
            var main = new Main();
            main.Show();
        }

        private async Task<AuthResponse> DoLogin()
        {
            _authResponse = new AuthResponse();
            // Generates state and PKCE values.
            var state = Core.Auth.Google.RandomDataBase64Url(32);
            var codeVerifier = Core.Auth.Google.RandomDataBase64Url(32);
            var codeChallenge = Core.Auth.Google.Base64UrlencodeNoPadding(Core.Auth.Google.Sha256(codeVerifier));
            const string codeChallengeMethod = "S256";

            // Creates a redirect URI using an available port on the loopback address.
            var redirectUri = $"http://{IPAddress.Loopback}:{Core.Auth.Google.GetRandomUnusedPort()}/";

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            http.Start();

            // Creates the OAuth 2.0 authorization request.
            var authorizationRequest =
                $"{AuthorizationEndpoint}?response_type=code&scope=openid%20profile&redirect_uri={System.Uri.EscapeDataString(redirectUri)}&client_id={ClientId}&state={state}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}";
            Browser.Navigate(authorizationRequest);
            // Opens request in the browser.
            //System.Diagnostics.Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Brings this app back to the foreground.
            // this.Activate();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            var responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            var responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
                Console.WriteLine("HTTP server stopped.");
            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                return _authResponse;
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {
                return _authResponse;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
            var incomingState = context.Request.QueryString.Get("state");

            // Compares the receieved state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incomingState != state)
            {
                return _authResponse;
            }
            // Starts the code exchange at the Token Endpoint.
            _authResponse = await Core.Auth.Google.PerformCodeExchange(code, codeVerifier, redirectUri);
            return _authResponse;
        }
    }
}
