using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace AppBanwao.KaryKart.Web.Helpers
{

    public class ApiHelper
    {
        string _apiUrl = ConfigurationManager.AppSettings["APIMainURL"].ToString();
        HttpClient _client = null;

        public static List<string> InvalidJsonElements;

        public IList<T> DeserializeToList<T>(string jsonString)
        {
            InvalidJsonElements = null;
            var array = JArray.Parse(jsonString);
            IList<T> objectsList = new List<T>();

            foreach (var item in array)
            {
                try
                {
                    // CorrectElements
                    objectsList.Add(item.ToObject<T>());
                }
                catch (Exception ex)
                {
                    InvalidJsonElements = InvalidJsonElements ?? new List<string>();
                    InvalidJsonElements.Add(item.ToString());
                }
            }

            return objectsList;
        }



        public ApiHelper()
        {

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_apiUrl);
        }

        public string SendRequest(string apiname)
        {
            try
            {
                return _client.GetAsync(apiname).Result.Content.ReadAsStringAsync().Result;
                //JsonConvert.DeserializeObject < List < API.Models.ProductModel >>(_client.GetAsync(apiname).Result.Content.ReadAsStringAsync().ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}