using Microsoft.AspNetCore.Mvc;
using Tinpurrs.Models;

namespace Tinpurrs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BreedsController: ControllerBase
    {
        private static readonly Dictionary<string, HashSet<string>> _sessionSeenCats = new();
        [HttpGet]
        public async Task<IActionResult> GetBreeds([FromQuery] int page, [FromQuery] int limit)
        {
            if (!Request.Cookies.TryGetValue("cat_session", out string sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                Response.Cookies.Append("cat_session", sessionId);
            }

            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "breeds.json");
            var json = await System.IO.File.ReadAllTextAsync(jsonPath);


            //using var client = new HttpClient();
            //string url = $"https://api.thecatapi.com/v1/breeds?limit={limit}&page={page}";
            //var response = await client.GetStringAsync(url);
            var breeds = System.Text.Json.JsonSerializer.Deserialize<List<CatBreed>>(json);
            //breeds = breeds.OrderBy(b => random.Next()).ToList();

            //var result = breeds.Select(b => new
            //{
            //    name = b.Name,
            //    temperament = b.Temperament,
            //    description = b.Description,
            //    origin = b.Origin,
            //    image_url = $"https://cdn2.thecatapi.com/images/{b.ReferenceImageId}.jpg"
            //});


            //var paginated = breeds
            //.Skip((page - 1) * limit)
            //.Take(limit)
            //.Select(b => new
            //{
            //    name = b.Name,
            //    temperament = b.Temperament,
            //    description = b.Description,
            //    origin = b.Origin,
            //    image_url = $"https://cdn2.thecatapi.com/images/{b.ReferenceImageId}.jpg"
            //});

            if (!_sessionSeenCats.TryGetValue(sessionId, out var seen))
            {
                seen = new HashSet<string>();
                _sessionSeenCats[sessionId] = seen;
            }

            // remove already seen cats
            breeds = breeds.Where(b => !seen.Contains(b.Name)).ToList();
            var random = new Random();

            // pick random cats as before
            if (breeds.Count == 0)
            {
                return Ok(new { message = "No more cats available for this session." });
            }

            breeds = breeds.OrderBy(b => random.Next()).ToList();

            // select and paginate
            var paginated = breeds
                .Take(limit)
                .Select(b =>
                {
                    seen.Add(b.Name); // mark these cats as seen
                    return new
                    {
                        name = b.Name,
                        temperament = b.Temperament,
                        description = b.Description,
                        origin = b.Origin,
                        image_url = $"https://cdn2.thecatapi.com/images/{b.ReferenceImageId}.jpg"
                    };
                });


            return Ok(paginated);

            //return Ok(result);

        }
    }
}
