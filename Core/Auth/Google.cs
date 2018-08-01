using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Newtonsoft.Json;

namespace Core.Auth
{
    public static class Google
    {
        private const string ClientId = "133086194188-h89cbj49flgc1jiaddt1d7ep4sbpupt1.apps.googleusercontent.com";
        private const string ClientSecret = "hnzEph626Sdo_Jj2vVMYqHKf";
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        private const string TokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        private const string UserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private static AuthResponse _authResponse;
        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        

        public static async Task<AuthResponse> PerformCodeExchange(string code, string codeVerifier, string redirectUri)
        {
            _authResponse = new AuthResponse();
            Output("Exchanging code for tokens...");

            // builds the  request
            var tokenRequestUri = "https://www.googleapis.com/oauth2/v4/token";
            var tokenRequestBody =
                $"code={code}&redirect_uri={System.Uri.EscapeDataString(redirectUri)}&client_id={ClientId}&code_verifier={codeVerifier}&client_secret={ClientSecret}&scope=&grant_type=authorization_code";

            // sends the request
            var tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestUri);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            var byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = byteVersion.Length;
            var stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(byteVersion, 0, byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                var tokenResponse = await tokenRequest.GetResponseAsync();
                using (var reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    var responseText = await reader.ReadToEndAsync();
                    Output(responseText);

                    // converts to dictionary
                    var tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                    var accessToken = tokenEndpointDecoded["access_token"];
                    return await UserinfoCall(accessToken);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Output("HTTP: " + response.StatusCode);
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            var responseText = await reader.ReadToEndAsync();
                            _authResponse.UserInfo=JsonConvert.DeserializeObject<UserInformation>(responseText);
                            return _authResponse;
                        }
                    }

                }
            }

            return null;
        }


        private static async Task<AuthResponse> UserinfoCall(string accessToken)
        {
            Output("Making API Call to Userinfo...");

            // builds the  request
            var userinfoRequestUri = "https://www.googleapis.com/oauth2/v3/userinfo";

            // sends the request
            var userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestUri);
            userinfoRequest.Method = "GET";
            userinfoRequest.Headers.Add($"Authorization: Bearer {accessToken}");
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            // gets the response
            var userinfoResponse = await userinfoRequest.GetResponseAsync();
            using (var userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
            {
                try
                {
                    // reads response body
                    var userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
                    _authResponse.UserInfo = JsonConvert.DeserializeObject<UserInformation>(userinfoResponseText);
                    _authResponse.IsAuthenticated = true;
                    return _authResponse;
                }
                catch (Exception err)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Appends the given string to the on-screen log, and the debug console.
        /// </summary>
        /// <param name="output">string to be appended</param>
        public static void Output(string output)
        {
            //textBoxOutput.Text = textBoxOutput.Text + output + Environment.NewLine;
            Console.WriteLine(output);
        }

        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        public static string RandomDataBase64Url(uint length)
        {
            var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlencodeNoPadding(bytes);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng"></param>
        /// <returns></returns>
        public static byte[] Sha256(string inputStirng)
        {
            var bytes = Encoding.ASCII.GetBytes(inputStirng);
            var sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }

        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Base64UrlencodeNoPadding(byte[] buffer)
        {
            var base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }
    }


}
