using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BoardGame.Server.Controllers
{
    [ApiController]
    [Route("api/boardgames")]
    public class BoardGamesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public BoardGamesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoardGame(int id)
        {
            string url = $"https://boardgamegeek.com/xmlapi2/thing?id={id}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Error fetching from BBG");
            }
                var xmlContent = await response.Content.ReadAsStringAsync();
                var doc = XDocument.Parse(xmlContent);
            var nameElement = doc.Descendants("name")
            .FirstOrDefault(x => (string)x.Attribute("type") == "primary");
             
            var yearElement = doc.Descendants("yearpublished").FirstOrDefault();
            var data = new
            { 
                Id = id,
                Name = nameElement?.Value,
                Year = yearElement?.Attribute("value")?.Value
            };
            return Ok(data);

        }
    }
}
