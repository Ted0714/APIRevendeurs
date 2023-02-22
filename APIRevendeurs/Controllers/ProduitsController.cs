using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APIRevendeurs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProduitsController : ControllerBase
    {
        // GET: produits
        [HttpGet]
        [Authorize]
        public async Task<List<Produits>> GetAsync()
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync("https://63f39eecde3a0b242b461bef.mockapi.io/api/v1/products");
            var data = JsonConvert.DeserializeObject<List<Produits>>(json);
            return data;
        }

        // GET produits/id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<Produits> Get(int id)
        {
            HttpClient client = new HttpClient();
            var json = await client.GetStringAsync("https://63f39eecde3a0b242b461bef.mockapi.io/api/v1/products/" + id);
            var data = JsonConvert.DeserializeObject<Produits>(json);
            return data;
        }
    }
}