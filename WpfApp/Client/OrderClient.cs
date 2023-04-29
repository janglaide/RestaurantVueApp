using System.Net.Http;
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp.Client
{
    public class OrderClient : GenericClient
    {
        public Order AddDishToOrder(int orderId, int dishId)
        {
            Order result;
            var name = nameof(Order);
            using var client = new HttpClient();
            var response = client
                .PostAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}/{orderId}/AddDish/{dishId}", new StringContent(""))
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<Order>(content.ReadAsStringAsync().Result);
            }

            return result;
        }

        public Order RemoveDishFromOrder(int orderId, int dishId)
        {
            Order result;
            var name = nameof(Order);
            using var client = new HttpClient();
            var response =  client
                .DeleteAsync($"{MainUrlSegment}{UrlSegmentDictionary[name]}/{orderId}/RemoveDish/{dishId}")
                .Result;

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using (var content = response.Content)
            {
                result = JsonConvert.DeserializeObject<Order>(content.ReadAsStringAsync().Result);
            }

            return result;
        }
    }
}
