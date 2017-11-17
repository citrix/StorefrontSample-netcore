using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

    public partial class Storefront
    {
        public async static Task<List<CitrixApplicationInfo>> GetResources(string WebURL, CitrixAuthCredential Creds, bool UseSSL)
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

            var _resources = await GetResourcesFromStoreFront(SFURL, Creds, UseSSL);

            return _resources;
        }
        public async static Task<List<CitrixApplicationInfo>> GetResources(string Server, string WebLocation, CitrixAuthCredential Creds, bool UseSSL)
        {
            string SFURL = null;

            if (WebLocation.StartsWith("/"))
            {
                WebLocation = WebLocation.Substring(1, WebLocation.Length - 1);
            }

            if (UseSSL)
            {
                SFURL = string.Format("https://{0}/{1}", Server, WebLocation);
            }
            else
            {
                SFURL = string.Format("http://{0}/{1}", Server, WebLocation);
            }

            var _resources = await GetResourcesFromStoreFront(SFURL, Creds, UseSSL);

            return _resources;

        }
        public async static Task<CitrixApplicationInfo> GetResource(string WebURL, CitrixAuthCredential Creds, bool UseSSL, string ApplicationName)
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

            var _resources = await GetResourcesFromStoreFront(SFURL, Creds, UseSSL);

            CitrixApplicationInfo _app = _resources.Find(a => a.AppTitle == ApplicationName);

            return _app;
        }
        public async static Task<CitrixApplicationInfo> GetResource(string Server, string WebLocation, CitrixAuthCredential Creds, bool UseSSL, string ApplicationName)
        {
            string SFURL = null;

            if (WebLocation.StartsWith("/"))
            {
                WebLocation = WebLocation.Substring(1, WebLocation.Length - 1);
            }

            if (UseSSL)
            {
                SFURL = string.Format("https://{0}/{1}", Server, WebLocation);
            }
            else
            {
                SFURL = string.Format("http://{0}/{1}", Server, WebLocation);
            }

            var _resources = await GetResourcesFromStoreFront(SFURL, Creds, UseSSL);

            CitrixApplicationInfo _app = _resources.Find(a => a.AppTitle == ApplicationName);

            return _app;
        }
        private async static Task<List<CitrixApplicationInfo>> GetResourcesFromStoreFront(string SFURL, CitrixAuthCredential Creds, bool UseSSL)
        {
            List<CitrixApplicationInfo> _applicationList = new List<CitrixApplicationInfo>();

            CookieContainer _cookieContainer = new CookieContainer();

            Cookie _aspnetSessionIdCookie = new Cookie("ASP.NET_SessionId", Creds.SessionID,Creds.CookiePath,Creds.CookieHost);
            Cookie _csrfTokenCookie = new Cookie("CsrfToken", Creds.CSRFToken, Creds.CookiePath, Creds.CookieHost);
            Cookie _authIDCookie = new Cookie("CtxsAuthId", Creds.AuthToken, Creds.CookiePath, Creds.CookieHost);
            _cookieContainer.Add(_aspnetSessionIdCookie);
            _cookieContainer.Add(_csrfTokenCookie);
            _cookieContainer.Add(_authIDCookie);

            HttpClientHandler _clientHandler = new HttpClientHandler();
            _clientHandler.CookieContainer = _cookieContainer;

            System.Net.Http.HttpClient _client = new System.Net.Http.HttpClient(_clientHandler);
            _client.BaseAddress = new Uri(SFURL);

            string _postResourceUrl = (SFURL.EndsWith("/")) ? "Resources/List" : "/Resources/List";
            
            string _resourcesURL = SFURL + _postResourceUrl;

            if (UseSSL)
            {
                _client.DefaultRequestHeaders.Add("X-Citrix-IsUsingHTTPS", "Yes");
            }
            else
            {
                _client.DefaultRequestHeaders.Add("X-Citrix-IsUsingHTTPS", "No");
            }

            _client.DefaultRequestHeaders.Add("Csrf-Token", Creds.CSRFToken);
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            StringContent _bodyContent = new StringContent("");

            HttpResponseMessage _listResourcesResp = await _client.PostAsync(_resourcesURL, _bodyContent);

            if (_listResourcesResp.StatusCode == HttpStatusCode.OK)
            {
                string _resourcesJSON = await _listResourcesResp.Content.ReadAsStringAsync();

                JObject _resourcesBase = JObject.Parse(_resourcesJSON);

                _applicationList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CitrixApplicationInfo>>(_resourcesBase["resources"].ToString());

                foreach ( var _resource in _applicationList)
                {
                    _resource.Auth = Creds;
                    //_resource.StorefrontURL;
                }
            }

            return _applicationList;
        }

    }

