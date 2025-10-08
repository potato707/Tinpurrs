using Microsoft.AspNetCore.Mvc;
using Tinpurrs.Models;

namespace Tinpurrs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedsController: ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBreeds([FromQuery] int page, [FromQuery] int limit)
        {
            using var client = new HttpClient();
            string url = $"https://api.thecatapi.com/v1/breeds?limit={limit}&page={page}";
            var response = await client.GetStringAsync(url);
            var breeds = System.Text.Json.JsonSerializer.Deserialize<List<CatBreed>>(response);
            var result = breeds.Select(b => new
            {
                name = b.Name,
                temperament = b.Temperament,
                description = b.Description,
                origin = b.Origin,
                image_url = $"https://cdn2.thecatapi.com/images/{b.ReferenceImageId}.jpg"
            });

            return Ok(result);

        }
    }
}
