using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;

public partial class Storefront
	{
		public async static Task<byte[]> GetImage(string WebURL, CitrixApplicationInfo Application)
        {
            string SFURL = WebURL;
            bool IsSSL = false;
            if (SFURL.ToLower().IndexOf("https:") != -1)
            {
                IsSSL = true;
            }
            else
            {
                IsSSL = false;
            }

            var _image = await GetImageFromStorefront(SFURL, Application);

            return _image;
        }
        public async static Task<byte[]> GetImage(string Server, string WebLocation, CitrixApplicationInfo Application, bool IsSSL)
        {
            string SFURL = null;

            if (WebLocation.StartsWith("/"))
            {
                WebLocation = WebLocation.Substring(1, WebLocation.Length - 1);
            }
            if (IsSSL)
            {
                SFURL = string.Format("https://{0}/{1}", Server, WebLocation);
            }
            else
            {
                SFURL = string.Format("http://{0}/{1}", Server, WebLocation);
            }

            var _image = await GetImageFromStorefront(SFURL, Application);

            return _image;
        }
        private async static Task<byte[]> GetImageFromStorefront(string SFURL, CitrixApplicationInfo Application)
		{
            CookieContainer _cookieContainer = new CookieContainer();

            Cookie _aspnetSessionIdCookie = new Cookie("ASP.NET_SessionId", Application.Auth.SessionID,Application.Auth.CookiePath,Application.Auth.CookieHost);
            Cookie _csrfTokenCookie = new Cookie("CsrfToken", Application.Auth.CSRFToken, Application.Auth.CookiePath, Application.Auth.CookieHost);
            Cookie _authIDCookie = new Cookie("CtxsAuthId", Application.Auth.AuthToken, Application.Auth.CookiePath, Application.Auth.CookieHost);
            _cookieContainer.Add(_aspnetSessionIdCookie);
            _cookieContainer.Add(_csrfTokenCookie);
            _cookieContainer.Add(_authIDCookie);

            HttpClientHandler _clientHandler = new HttpClientHandler();
            _clientHandler.CookieContainer = _cookieContainer;

            HttpClient _client = new HttpClient(_clientHandler);
            _client.BaseAddress = new Uri(Application.Auth.StorefrontUrl);

            string _postResourceUrl = (SFURL.EndsWith("/")) ? "Resources/List" : "/Resources/List";
            
            string _resourcesURL = SFURL + _postResourceUrl;

            _client.DefaultRequestHeaders.Add("Csrf-Token", Application.Auth.CSRFToken);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("image/png"));

			string _imageResource = (SFURL.EndsWith("/")) ? string.Format("{0}",Application.AppIconUrl) : string.Format("/{0}", Application.AppIconUrl);

			string _imageResourceUrl = string.Format("{0}{1}", SFURL, _imageResource);

			StringContent _bodyContent = new StringContent("");

			HttpResponseMessage _imageResp = await _client.GetAsync(_imageResourceUrl);

            byte[] imageBytes = null;
            Debug.WriteLine("StatusCode:" + _imageResp.StatusCode);
            if ( _imageResp.StatusCode == System.Net.HttpStatusCode.OK )
            {
                            
			    imageBytes = await _imageResp.Content.ReadAsByteArrayAsync();
            }

			return imageBytes;
		}
        public static string GetBase64(byte[] Image)
        {
            return Convert.ToBase64String(Image);
        }
	}
