using Microsoft.AspNetCore.Mvc;


namespace Net6Mimi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
           string filename=  file.Name;
            if (!Directory.Exists(Path.Combine("./Image")))
            {
                Directory.CreateDirectory(Path.Combine("./Image"));
            }
            string filePath = Path.Combine("./Image", "abc.jpg");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Ok(1);
        }

        [HttpPost]
        public IActionResult Download()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "abc.jpg");
            Console.WriteLine(filePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return  new FileContentResult(fileBytes, "image/jpeg");

        }
    }
}
