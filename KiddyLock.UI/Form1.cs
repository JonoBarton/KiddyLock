using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Core.Models;
using KiddyLock.UI.Controls;
using KiddyLock.UI.Logic;

namespace KiddyLock.UI
{
    public partial class Form1 : Form
    {
        private List<User> _osUsers;
        private List<User> _users;
        private User _selectedUser;
        private AuthResponse _authResponse;
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string TokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        private const string UserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private const string ClientId = "133086194188-h89cbj49flgc1jiaddt1d7ep4sbpupt1.apps.googleusercontent.com";
        private const string FORGE_CALLBACK_URL = "http://fake.com/api/forge/callback/oauth";
        private WebBrowser2 wb = new WebBrowser2();
        public Form1()
        {
            InitializeComponent();
            Closed += Form1_Closed;
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            MainPanel.Visible = false;
            _authResponse = await DoLogin();
            if (!_authResponse.IsAuthenticated)
            {
                MessageBox.Show("Unable to authenticate");
            }
            MainPanel.Visible = true;
            Controls.Remove(wb);
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
            wb.Dock = DockStyle.Fill;
            Controls.Add(wb);
            wb.Navigate(authorizationRequest);
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

       
        private async void GetUsers()
        {
            _osUsers = UserLogic.GetOsUsers();
            _users = await UserLogic.GetUsers();

            var userBinding = new BindingList<User>();
            foreach (var user in _osUsers)
            {
                userBinding.Add(user);
            }
            UserCombo.DataSource = userBinding;
            UserCombo.DisplayMember = "Name";
            UserCombo.ValueMember = "Sid";
        }

        private void UserCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            _selectedUser = _osUsers[comboBox.SelectedIndex];
        }

        private void UsersButton_Click(object sender, EventArgs e)
        {
            GetUsers();
        }
    }
}
