using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TradeX
{            
    internal sealed class HttpService
    {
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private string BaseUrl = "";
#if RELEASE
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private string Token = "";                                      

        public HttpService(Parameters xParameters)
        {
            this.BaseUrl = xParameters.BaseUrl;
        }

        public string GetToken()
        {
            return Token;
        }

        public void SetToken(string token)
        {
            this.Token = token;
        }


        //TODO: Only Shah Investor's Home Ltd
        private string GetClientUserAgent()
        {
            string productname = "-";
            string productversion = "-";

            string manufacturer = "-";
            string model = "-";
            string osName = "-";
            string osVersion = "-";
            string sdkNetVersion = "-";

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Manufacturer, Model FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        manufacturer = obj["Manufacturer"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        model = obj["Model"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }

            try
            {
                ManagementObjectSearcher searcher2 = new ManagementObjectSearcher("SELECT Caption, Version FROM Win32_OperatingSystem");
                foreach (ManagementObject obj in searcher2.Get())
                {
                    try
                    {
                        osName = obj["Caption"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        osVersion = obj["Version"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                sdkNetVersion = Environment.Version.ToString();    // sdk version
            }
            catch (Exception ex)
            {
            }

            try
            {
                productname = "TradeXClient";
                productversion = "1.0.0";
            }
            catch (Exception ex)
            {
            }
            var client_user_agent = string.Format("{0}/{1} ({2} {3}; {4} {5} {6})", productname, productversion, manufacturer, model, osName, osVersion, sdkNetVersion);
            return client_user_agent;
        }


        public Task<HttpResponseMessage> PostAsync(string path, Dictionary<string, string> bodyList)
        {
            Uri url = new Uri(BaseUrl + path);

            HttpClient httpClient = new HttpClient();
            var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(bodyList), Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(Token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer "+Token);
            }
            //TODO: Only Shah Investor's Home Ltd
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(GetClientUserAgent());

            return httpClient.PostAsync(url.OriginalString, stringContent);
        }

        public Task<HttpResponseMessage> PostAsync(string path, string body)
        {
            Uri url = new Uri(BaseUrl + path);

            HttpClient httpClient = new HttpClient();
            var stringContent = new StringContent(body, Encoding.UTF8, "application/json");
            if (!string.IsNullOrEmpty(Token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }
            //TODO: Only Shah Investor's Home Ltd
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(GetClientUserAgent());

            return httpClient.PostAsync(url.OriginalString, stringContent);
        }

        public Task<HttpResponseMessage> PostAsync<T>(string path, Dictionary<string, string> bodyList = null, Dictionary<string, string> parametersList = null)
        {
            Uri url = new Uri(BaseUrl + path);

            HttpClient httpClient = new HttpClient();
            if (parametersList != null)
            {
                foreach (var item in parametersList)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            if (!string.IsNullOrEmpty(Token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }
            //TODO: Only Shah Investor's Home Ltd
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(GetClientUserAgent());


            var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(bodyList), Encoding.UTF8, "application/json");

            return httpClient.PostAsync(url.OriginalString, stringContent);
        }

        public Task<HttpResponseMessage> GetAsync<T>(string path, Dictionary<string, string> parametersList = null)
        {
            Uri url = new Uri(BaseUrl + path);

            HttpClient httpClient = new HttpClient();
            if (parametersList != null)
            {
                foreach (var item in parametersList)
                {
                    httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
            if (!string.IsNullOrEmpty(Token))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            }
            //TODO: Only Shah Investor's Home Ltd
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(GetClientUserAgent());

            return httpClient.GetAsync(url.OriginalString);
        }
    }
}
