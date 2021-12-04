using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
namespace Mvc
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient();
        static GlobalVariables()
        {
            WebApiClient.BaseAddress = new Uri("http://localhost:62205/api/");
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}