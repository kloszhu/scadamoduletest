using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Xml;

namespace GBK2Utf8.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult GetRequest()
        {
            XmlDocument doc= new XmlDocument();
            doc.LoadXml(@"<root>
            <Name>周杰伦</Name>
<Code>周杰伦1</Code>
</root>");
            using (StreamWriter writer=new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"abc.txt"),true,Encoding.UTF8))
            {
                writer.WriteLine(JsonConvert.SerializeXmlNode(doc));
                writer.Flush();
            }

            ///找到格式了自己转化  gbk8-utf8
            return Ok();
        }
        public class Hello
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

    }
}
