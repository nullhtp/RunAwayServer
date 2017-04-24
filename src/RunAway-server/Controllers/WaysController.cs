using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core;

namespace RunAway_server.Controllers
{
    [Route("api/[controller]")]
    public class WaysController : Controller
    {
        public List<Way> Ways { get; set; }

        // GET api/values
        private void Init()
        {
            City kaliningrad = new City
            {
                Id = "df679694-d505-4dd3-b514-4ba48c8a97d8",
                Name = "Kenigsberg"
            };

            Point[] points = {
                new Point { Latitude = 56.399625, Longitude = 36.71120, Description="1"},
                new Point { Latitude = 55.751574, Longitude = 37.573856, Description="1"},
                new Point { Latitude = 55.819543, Longitude = 37.611619, Description="1"},
            };

            Ways = new List<Way>();
            Ways.Add(new Way { Id = 1, Category = Category.Run, Description = "1 rout this is full description about 100 sign maby more", Likes = 5, Name = "Rout 1", Points = points, Distance = 5.3, City = kaliningrad });
            Ways.Add(new Way { Id = 2, Category = Category.Walk, Description = "2 rout this is full description about 100 sign maby more", Likes = 15, Name = "Rout 2", Points = points, Distance = 2.3, City = kaliningrad });
            Ways.Add(new Way { Id = 3, Category = Category.Cycle, Description = "3 rout this is full description about 100 sign maby more", Likes = 4, Name = "Rout 3", Points = points, Distance = 1.1, City = kaliningrad });
            Ways.Add(new Way { Id = 4, Category = Category.Run, Description = "4 rout this is full description about 100 sign maby more", Likes = 9, Name = "Rout 4", Points = points, Distance = 4.0, City = kaliningrad });
            Ways.Add(new Way { Id = 5, Category = Category.Walk, Description = "5 rout this is full description about 100 sign maby more", Likes = 0, Name = "Rout 5", Points = points, Distance = 0.2, City = kaliningrad });
            Ways.Add(new Way { Id = 6, Category = Category.Cycle, Description = "6 rout this is full description about 100 sign maby more", Likes = 0, Name = "Rout 6", Points = points, Distance = 2.9, City = kaliningrad });
            Ways.Add(new Way { Id = 7, Category = Category.Run, Description = "7 rout this is full description about 100 sign maby more", Likes = 3, Name = "Rout 7", Points = points, Distance = 2.4, City = kaliningrad });
        }

        [HttpGet]
        public JsonResult Get()
        {
            Init();
            return Json(Ways);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Init();
            return Json(Ways.FirstOrDefault(way => way.Id == id));
        }

        [HttpGet("{city}/{category}")]
        public JsonResult Get(string city, int category = 0)
        {
            Init();
            Ways = category != 0
                ? Ways.FindAll(way => way.City.Id == city && way.Category.Equals(category))
                : Ways.FindAll(way => way.City.Id == city);
            return Json(Ways);
        }

        // POST api/values
        [HttpPost]
        public int Post([FromBody]Way way)
        {
            Init();
            way.Id = Ways.Max(w => w.Id) + 1;
            return way.Id;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
