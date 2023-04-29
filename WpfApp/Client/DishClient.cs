using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp.Client
{
    public class DishClient : GenericClient
    {
        public IList<DishWithIngredients> GetByName(string searchName)
        {
            List<DishWithIngredients> result;
            using var client = new HttpClient();
            var name = nameof(Dish);
            var response = client
                .GetAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}/GetByName/{searchName}")
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<List<DishWithIngredients>>(content.ReadAsStringAsync().Result);
            }

            return result;
        }
    }
}
