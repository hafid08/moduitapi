using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModuitAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private HttpClient client;
        public QuestionController(IHttpClientFactory factory)
        {
            this.client = factory.CreateClient("moduit");
        }
        [HttpGet("one")]
        public async Task<IActionResult> One()
        {
            var response = await client.GetAsync(GlobalVar.OneEndPoint);
            var contents = await response.Content.ReadAsStringAsync();
            return Ok(JsonSerializer.Deserialize<ModuitModel>(contents));
        }
        [HttpGet("two")]
        public async Task<IActionResult> Two()
        {
            string filter = "Ergonomics";
            string tag = "Sports";
            var response = await client.GetAsync(GlobalVar.TwoEndPoint);
            var contents = await response.Content.ReadAsStringAsync();
            var result = (JsonSerializer.Deserialize<List<ModuitModel>>(contents))
                .Where(x => (x.description.Contains(filter) || x.title.Contains(filter)) && (x.tags != null && x.tags.Contains(tag))).OrderByDescending(o => o.id).Take(3);
            return Ok(result);
        }
        [HttpGet("three")]
        public async Task<IActionResult> Three()
        {
            var response = await client.GetAsync(GlobalVar.ThreeEndPoint);
            var contents = await response.Content.ReadAsStringAsync();
            var list = (JsonSerializer.Deserialize<List<MainModel>>(contents));
            List<dynamic> result = new List<dynamic>();
            foreach(var main in list)
            {
                if(main.items != null && main.items.Count() > 0)
                {
                    List<ModuitModel> items = new List<ModuitModel>();
                    foreach (var itm in main.items)
                    {
                        items.Add(new ModuitModel() {
                            id = main.id,
                            category = main.category,
                            title = itm.title,
                            description = itm.description,
                            footer = itm.footer,
                            createdAt = main.createdAt
                        });
                    }
                    result.Add(items);
                } else
                {
                    result.Add(main);
                }
            }
            return Ok(result);
        }
    }
}
