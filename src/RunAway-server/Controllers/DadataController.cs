using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace RunAway_server.Controllers
{


    [Route("api/[controller]")]
    public class DadataController : Controller
    {
        private string _token = "a7af8472a7583e5913c7aa5741969f504918c049";
        private string _addressUrl = "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address";

        public string Get()
        {

            return "Ok";
        }

        // GET api/values/5
        [HttpGet("{query}")]
        public async Task<JsonResult> Get(string query)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders
                  .Authorization = new AuthenticationHeaderValue("Token", this._token);

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    query = query,
                    from_bound = new { value = "city" },
                    to_bound = new { value = "city" }
                }),
                Encoding.UTF8,
                "application/json");

            var response = client.PostAsync(this._addressUrl, content).Result;
            var result = await response.Content.ReadAsStringAsync();
            var citiesJson = (JObject)JsonConvert.DeserializeObject(result);
            List<City> cities = new List<City>();
            foreach (var city in citiesJson.First.First)
            {
                var Id = city["data"]["city_fias_id"].Value<string>();
                var Name = city["data"]["city_with_type"].Value<string>();
                var RegionName = city["data"]["region_with_type"].Value<string>();
                cities.Add(new City { Id = Id, Name = Name, RegionName= RegionName });
            }
            var json = Json(cities);
            return json;
        }

    }

}
