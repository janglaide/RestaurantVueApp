using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace WpfApp.Client
{
    public class GenericClient
    {
        public const string MainUrlSegment = "https://localhost:5001/api/";
        private static GenericClient _instance = null!;
        public GenericClient()
        {
            _instance ??= this;
        }
        public readonly Dictionary<string, string> UrlSegmentDictionary = new Dictionary<string, string>()
        {
            {"Dish", "dish"},
            {"Menu", "menu"},
            {"MenuDishes", "menu"},
            {"Order", "order"},
        };
        public IList<T> GetList<T>()
            where T : class, new()
        {
            List<T> result;
            using var client = new HttpClient();
            var name = typeof(T).Name;
            var response = client
                .GetAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}")
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<List<T>>(content.ReadAsStringAsync().Result);
            }

            return result;
        }

        public T Get<T>(int id)
            where T : class, new()
        {
            T result;
            using var client = new HttpClient();
            var name = typeof(T).Name;
            var response = client
                .GetAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}/{id}")
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<T>(content.ReadAsStringAsync().Result);
            }

            return result;
        }

        public T GetByName<T>(string searchName)
         where T : class, new()
        {
            T result;
            using var client = new HttpClient();
            var name = typeof(T).Name;
            var response = client
                .GetAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}/GetByName/{searchName}")
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<T>(content.ReadAsStringAsync().Result);
            }

            return result;
        }
    }
}
